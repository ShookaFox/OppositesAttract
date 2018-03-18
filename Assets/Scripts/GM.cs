using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public GameObject fpsController;
    public GameObject cinematicCamera;

    public static GameState currentState = GameState.INTRO;

    // Use this for initialization
    void Start()
    {
        if (currentState == GameState.PLAYING)
        {
            Time.timeScale = 1;
            fpsController.SetActive(true);
            cinematicCamera.SetActive(false);
        }
        else
        {
            fpsController.SetActive(false);
            cinematicCamera.SetActive(true);
            //Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.PLAYING)
        {
            Time.timeScale = 1;
            fpsController.SetActive(true);
            cinematicCamera.SetActive(false);
        }
        else
        {
            fpsController.SetActive(false);
            cinematicCamera.SetActive(true);
            //Time.timeScale = 0;
        }
    }

    public void OnIntroComplete()
    {
        currentState = GameState.PLAYING;
    }
}

public enum GameState
{
    PLAYING,
    INTRO,
    PAUSED
}