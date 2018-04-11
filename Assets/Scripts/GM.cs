using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Sprite toryNeutral;
    public Sprite toryAnnoyed;
    public Sprite toryConfident;
    public Sprite samNeutral;
    public Sprite samConfident;
    public Sprite samAwkward;
    public Sprite dannyNeutral;
    public Sprite silverNeutral;

    public Image emoteImage;

    public TMPro.TextMeshProUGUI settingText;

    DialogueSession dialogueSession;

    int gameIndex = 1;

    void Start()
    {
        dialogueSession = DialogueTreeParser.ParseDialogueJSON(dialogueJSON.text);
        dialogueSession.actions["startGame"].listeners.Add(StartGame);
        dialogueSession.actions["emote"].listeners.Add(EmoteUpdate);
        dialogueSession.actions["genericoHouse"].listeners.Add(genericoHouse);
        dialogueSession.actions["afterFirstGame"].listeners.Add(afterFirstGame);
        dialogueSession.actions["afterSecondGame"].listeners.Add(afterSecondGame);
        //dialogueSession.actions["afterLastGame"].listeners.Add(FinishedAction);

        dialogueManager.StartDialogue(dialogueSession);
        dialogueManager.dialogueSession.variables.Add("emote", "none");
    }

    bool StartGame()
    {
        soccerGameObject.SetActive(true);
        cinematicCamera.gameObject.SetActive(false);
        dialogueManager.dialogueSession.variables["emote"] = "none";
        dialogueManager.DismissDialogueBox();
        scoreboard.StartTimer();
        gameRunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        return false;
    }

    bool genericoHouse()
    {
        StartCoroutine(TypeSentence("Your House..."));
        return false;
    }

    bool afterFirstGame()
    {
        StartCoroutine(TypeSentence("Later, After The First Game..."));
        return false;
    }

    bool afterSecondGame()
    {
        StartCoroutine(TypeSentence("Later, After The Second Game..."));
        return false;
    }

    bool afterThirdGame()
    {
        StartCoroutine(TypeSentence("Later, After The Third Game..."));
        return false;
    }

    IEnumerator TypeSentence(string sentence)
    {
        settingText.text = "";
        dialogueManager.DismissDialogueBox();
        foreach (char letter in sentence.ToCharArray())
        {
            settingText.text += letter;
            yield return null;
        }
        float t = 0f;
        while (t < 3f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        settingText.text = "";

        dialogueManager.dialogueSession.currentID = dialogueManager.dialogueSession.dialogueNodes[dialogueManager.dialogueSession.currentID].next;
        dialogueManager.DealWithCurrentNode();
    }

    bool FinishedAction()
    {
        return true;
    }

    bool EmoteUpdate()
    {
        switch (dialogueManager.dialogueSession.variables["emote"])
        {
            case "none":
                emoteImage.gameObject.SetActive(false);
                break;
            case "toryNeutral":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = toryNeutral;
                break;
            case "toryAnnoyed":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = toryAnnoyed;
                break;
            case "toryConfident":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = toryConfident;
                break;
            case "samNeutral":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = samNeutral;
                break;
            case "samConfident":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = samConfident;
                break;
            case "samAwkward":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = samAwkward;
                break;
            case "dannyNeutral":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = dannyNeutral;
                break;
            case "silverNeutral":
                emoteImage.gameObject.SetActive(true);
                emoteImage.sprite = silverNeutral;
                break;
            default:
                emoteImage.gameObject.SetActive(false);
                break;
        }
        return true;
    }

    public void OnGameEnded(int homeScore, int visitorScore)
    {
        //dialogueManager.dialogueSession.actions["playSoccer"].finished = true;
        dialogueManager.dialogueSession.currentID = dialogueManager.dialogueSession.dialogueNodes[dialogueManager.dialogueSession.currentID].next;
        dialogueManager.DealWithCurrentNode();
        gameRunning = false;
        shouldReset = true;

        dialogueManager.dialogueSession.variables["game" + gameIndex + "Won"] = homeScore > visitorScore ? "1" : "0";
        
        scoreboard.StopTimer();
        scoreboard.ResetTimer();
        scoreboard.redScore = 0;
        scoreboard.blueScore = 0;
        soccerGameObject.SetActive(false);
        cinematicCamera.gameObject.SetActive(true);
        gameIndex++;
    }
    
    void Update()
    {
        if (scoreboard.timer > 0.16f)
        {
            shouldReset = false;
        }
        EmoteUpdate();
    }
}