using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;
    public TMPro.TextMeshProUGUI npcNameText;
    public TMPro.TextMeshProUGUI dialogueTextText;

    public Animator animator;

    void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Beginning conversation with " + dialogue.name);

        animator.SetBool("IsOpen", true);
        sentences.Clear();
        npcNameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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
        Debug.Log("End of Conversation!");
    }
}
