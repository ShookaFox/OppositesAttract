using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public float heightOfChoiceButton = 32.7f;
    public float spaceBetweenChoiceButtons = 5f;
    public float panelInitialSize = 280.37f;
    public GameObject dialoguePanel;

    public TMPro.TextMeshProUGUI npcNameText;
    public TMPro.TextMeshProUGUI dialogueTextText;

    public TMPro.TextMeshProUGUI option1Text;
    public TMPro.TextMeshProUGUI option2Text;
    public TMPro.TextMeshProUGUI option3Text;
    public TMPro.TextMeshProUGUI option4Text;

    public GameObject option1Btn;
    public GameObject option2Btn;
    public GameObject option3Btn;
    public GameObject option4Btn;

    public GameObject continueButton;

    public Animator dialoguePanelAnimator;

    DialogueSession dialogueSession;

    public void StartDialogue(DialogueSession dialogueSession)
    {
        this.dialogueSession = dialogueSession;
        dialoguePanelAnimator.SetBool("IsOpen", true);
        DealWithCurrentNode();
    }

    IEnumerator AnimateChoices(int numberOfChoices)
    {
        float lerpTime = 0f;
        int choiceID = 0;
        int choicesAlreadyShowing = 0;
        if (option1Btn.activeInHierarchy) choicesAlreadyShowing = 1;
        if (option2Btn.activeInHierarchy) choicesAlreadyShowing = 2;
        if (option3Btn.activeInHierarchy) choicesAlreadyShowing = 3;
        if (option4Btn.activeInHierarchy) choicesAlreadyShowing = 4;
        RectTransform diaTransform = dialoguePanel.GetComponent<RectTransform>();
        float origSize = diaTransform.sizeDelta.y;

        int choicesDiff = numberOfChoices - choicesAlreadyShowing;
        float sizeDiff = origSize - (numberOfChoices * (heightOfChoiceButton) + (Mathf.Clamp(Mathf.Abs(numberOfChoices) -1, 0, 4) * spaceBetweenChoiceButtons));

        Vector3 o1Pos = option1Btn.transform.position;
        Vector3 o2Pos = option2Btn.transform.position;
        Vector3 o3Pos = option3Btn.transform.position;
        Vector3 o4Pos = option4Btn.transform.position;

        float t = 0;
        while (choiceID <= 4)
        {
            lerpTime += Time.unscaledDeltaTime;
            t = lerpTime / 0.2f;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            if (choiceID == 0)
            {
                diaTransform.sizeDelta = new Vector2(diaTransform.sizeDelta.x, origSize + (sizeDiff * t));
            }

            if (choiceID == 1 && numberOfChoices > 0 && choicesAlreadyShowing < 1)
            {
                option1Btn.transform.position = new Vector3((o1Pos.x - 10) + (t * 10), o1Pos.y, o1Pos.z);
                option1Text.color = new Color(option1Text.color.r, option1Text.color.g, option1Text.color.b, t);
            }
            else
            {
                option1Btn.transform.position = new Vector3((o1Pos.x) + (t * -10), o1Pos.y, o1Pos.z);
                option1Text.color = new Color(option1Text.color.r, option1Text.color.g, option1Text.color.b, 1-t);
            }

            if (choiceID == 2 && numberOfChoices > 1 && choicesAlreadyShowing < 2)
            {
                option2Btn.transform.position = new Vector3((o2Pos.x - 10) + (t * 10), o2Pos.y, o2Pos.z);
                option2Text.color = new Color(option2Text.color.r, option2Text.color.g, option2Text.color.b, t);
            }
            else
            {
                option2Btn.transform.position = new Vector3((o2Pos.x) + (t * -10), o2Pos.y, o2Pos.z);
                option2Text.color = new Color(option2Text.color.r, option2Text.color.g, option2Text.color.b, 1-t);
            }

            if (choiceID == 3 && numberOfChoices > 2 && choicesAlreadyShowing < 3)
            {
                option3Btn.transform.position = new Vector3((o3Pos.x - 10) + (t * 10), o3Pos.y, o3Pos.z);
                option3Text.color = new Color(option3Text.color.r, option3Text.color.g, option3Text.color.b, t);
            }
            else
            {
                option3Btn.transform.position = new Vector3((o3Pos.x) + (t * -10), o3Pos.y, o3Pos.z);
                option3Text.color = new Color(option3Text.color.r, option3Text.color.g, option3Text.color.b, 1-t);
            }

            if (choiceID == 4 && numberOfChoices > 3 && choicesAlreadyShowing < 4)
            {
                option4Btn.transform.position = new Vector3((o4Pos.x - 10) + (t * 10), o4Pos.y, o4Pos.z);
                option4Text.color = new Color(option4Text.color.r, option4Text.color.g, option4Text.color.b, t);
            }
            else
            {
                option4Btn.transform.position = new Vector3((o4Pos.x) + (t * -10), o4Pos.y, o4Pos.z);
                option4Text.color = new Color(option4Text.color.r, option4Text.color.g, option4Text.color.b, 1-t);
            }

            if (t >= 1)
            {
                lerpTime = 0;
                choiceID++;
            }

            yield return null;
        }
        

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

                if (textDialogueNode.choices == null)
                {
                    continueButton.SetActive(true);
                }
                else
                {
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
        StartCoroutine(AnimateChoices(3));
    }

    public void EndDialogue()
    {
        dialoguePanelAnimator.SetBool("IsOpen", false);
        dialoguePanelAnimator.SetInteger("NumOfOptions", 0);
    }

    public void PlayButtonHoveredSound()
    {
        AudioManager.instance.playButtonHovered();
    }
}
