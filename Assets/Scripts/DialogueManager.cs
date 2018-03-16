using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI npcNameText;
    public TMPro.TextMeshProUGUI dialogueTextText;

    public TMPro.TextMeshProUGUI option1Text;
    public TMPro.TextMeshProUGUI option2Text;
    public TMPro.TextMeshProUGUI option3Text;
    public TMPro.TextMeshProUGUI option4Text;

    public GameObject continueButton;

    public Animator animator;

    public TextAsset testDialogue;

    DialogueSession dialogueSession;

    public void StartDialogue(DialogueSession dialogueSession)
    {
        this.dialogueSession = dialogueSession;
        animator.SetBool("IsOpen", true);
        DealWithCurrentNode();
    }

    public void Option1Selected()
    {
        AudioManager.instance.playButtonClicked();
        dialogueSession.currentID = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[((TextDialogueNode)dialogueSession.dialogueNodes[dialogueSession.currentID]).choices[0]]).next;
        DealWithCurrentNode();
    }

    public void Option2Selected()
    {
        AudioManager.instance.playButtonClicked();
        dialogueSession.currentID = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[((TextDialogueNode)dialogueSession.dialogueNodes[dialogueSession.currentID]).choices[1]]).next;
        DealWithCurrentNode();
    }

    public void Option3Selected()
    {
        AudioManager.instance.playButtonClicked();
        dialogueSession.currentID = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[((TextDialogueNode)dialogueSession.dialogueNodes[dialogueSession.currentID]).choices[2]]).next;
        DealWithCurrentNode();
    }

    public void Option4Selected()
    {
        AudioManager.instance.playButtonClicked();
        dialogueSession.currentID = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[((TextDialogueNode)dialogueSession.dialogueNodes[dialogueSession.currentID]).choices[3]]).next;
        DealWithCurrentNode();
    }

    public void Continue()
    {
        AudioManager.instance.playButtonClicked();
        dialogueSession.currentID = ((TextDialogueNode)dialogueSession.dialogueNodes[dialogueSession.currentID]).next;
        DealWithCurrentNode();
    }

    private void DealWithCurrentNode()
    {
        if (dialogueSession.currentID != null)
        {
            DialogueNode dialogueNode = dialogueSession.dialogueNodes[dialogueSession.currentID];
            if (dialogueNode is TextDialogueNode)
            {
                TextDialogueNode textDialogueNode = (TextDialogueNode)dialogueNode;

                npcNameText.text = textDialogueNode.character;

                StopAllCoroutines();
                StartCoroutine(TypeSentence(textDialogueNode.text));

                animator.SetInteger("NumOfOptions", textDialogueNode.choices.Length);

                if (textDialogueNode.choices.Length > 0)
                {
                    continueButton.SetActive(false);

                    option1Text.text = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[textDialogueNode.choices[0]]).title;

                    if (textDialogueNode.choices.Length > 1)
                    {
                        option2Text.text = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[textDialogueNode.choices[1]]).title;
                    }

                    if (textDialogueNode.choices.Length > 2)
                    {
                        option3Text.text = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[textDialogueNode.choices[2]]).title;
                    }

                    if (textDialogueNode.choices.Length > 3)
                    {
                        option4Text.text = ((ChoiceDialogueNode)dialogueSession.dialogueNodes[textDialogueNode.choices[3]]).title;
                    }
                }
                else
                {
                    continueButton.SetActive(false);
                }
            }
            else if (dialogueNode is SetDialogueNode)
            {
                SetDialogueNode setDialogueNode = (SetDialogueNode)dialogueNode;
                if (dialogueSession.variables.ContainsKey(setDialogueNode.variableName))
                {
                    dialogueSession.variables[setDialogueNode.variableName] = setDialogueNode.value;
                }
                else
                {
                    dialogueSession.variables.Add(setDialogueNode.variableName, setDialogueNode.value);
                }
                dialogueSession.currentID = setDialogueNode.next;
                DealWithCurrentNode();
            }
            else if (dialogueNode is BranchDialogueNode)
            {
                BranchDialogueNode branchDialogueNode = (BranchDialogueNode)dialogueNode;
                string variableValue = dialogueSession.variables[branchDialogueNode.variableCondition];

                bool foundOne = false;
                string newID = null;
                string defaultID = null;
                for (int i = 0; i < branchDialogueNode.branches.Length; i++)
                {
                    if (branchDialogueNode.branches[i].value != "_default")
                    {
                        if (variableValue == branchDialogueNode.branches[i].value)
                        {
                            foundOne = true;
                            newID = branchDialogueNode.branches[i].target;
                            break;
                        }
                    }
                    else
                    {
                        defaultID = branchDialogueNode.branches[i].target;
                    }
                }

                if (!foundOne)
                {
                    newID = defaultID;
                }

                dialogueSession.currentID = newID;
                DealWithCurrentNode();
            }
            else if (dialogueNode is ActionDialogueNode)
            {
                ActionDialogueNode actionDialogueNode = (ActionDialogueNode)dialogueNode;
                string eventName = actionDialogueNode.eventName;
                foreach (OnAction actionDelegate in dialogueSession.actions[eventName])
                {
                    actionDelegate.Invoke();
                }
            }
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTextText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTextText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        animator.SetInteger("NumOfOptions", 0);
    }

    public void PlayButtonHoveredSound()
    {
        AudioManager.instance.playButtonHovered();
    }
}
