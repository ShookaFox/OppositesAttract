using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour {
    public FadeOut fadeOut;

    public TextAsset dialogueDemoJSON;
    private bool tryingToPlaySoccer = false;

    private void Start()
    {
        DialogueSession dialogueSession = DialogueTreeParser.ParseDialogueJSON(dialogueDemoJSON.text);
        dialogueSession.actions["playSoccer"].Add(PlaySoccer);

        FindObjectOfType<DialogueManager>().StartDialogue(dialogueSession);
    }

    public void TriggerDialogue()
    {
        
    }

    public void PlaySoccer()
    {
        tryingToPlaySoccer = true;
        fadeOut.isFading = true;
    }

    private void Update()
    {
        if (tryingToPlaySoccer && fadeOut.finishedFading)
        {
            SceneManager.LoadScene(2);
        }
    }
}
