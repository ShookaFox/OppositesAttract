using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject fpsController;
    public CinematicCameraMovement cinematicCamera;
    public DialogueManager dialogueManager;
    public GameObject soccerGameObject;
    public Scoreboard scoreboard;

    public bool gameRunning = false;
    public bool shouldReset = false;
    
    public TextAsset dialogueJSON;

    DialogueSession dialogueSession;

    void Start()
    {
        dialogueSession = DialogueTreeParser.ParseDialogueJSON(dialogueJSON.text);
        dialogueSession.actions["playSoccer"].listeners.Add(StartGame);

        dialogueManager.StartDialogue(dialogueSession);
    }

    bool StartGame()
    {
        soccerGameObject.SetActive(true);
        cinematicCamera.gameObject.SetActive(false);
        dialogueManager.DismissDialogueBox();
        scoreboard.StartTimer();
        gameRunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        return false;
    }

    public void OnGameEnded(int homeScore, int visitorScore)
    {
        //dialogueManager.dialogueSession.actions["playSoccer"].finished = true;
        dialogueManager.dialogueSession.currentID = dialogueManager.dialogueSession.dialogueNodes[dialogueManager.dialogueSession.currentID].next;
        dialogueManager.DealWithCurrentNode();
        gameRunning = false;
        shouldReset = true;
        
        scoreboard.StopTimer();
        scoreboard.ResetTimer();
        scoreboard.redScore = 0;
        scoreboard.blueScore = 0;
        soccerGameObject.SetActive(false);
        cinematicCamera.gameObject.SetActive(true);
    }
    
    void Update()
    {
        if (scoreboard.timer > 0.16f)
        {
            shouldReset = false;
        }
    }
}