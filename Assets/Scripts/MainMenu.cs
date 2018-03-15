using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public FadeOut fadeOut;

    private bool tryingToPlay = false;
    private bool tryingToQuit = false;
    private bool tryingToDemoDialogue = false;

    public void PlayGame()
    {
        tryingToPlay = true;
        fadeOut.isFading = true;
    }

    public void DemoDialogue()
    {
        tryingToDemoDialogue = true;
        fadeOut.isFading = true;
    }

    public void QuitGame()
    {
        tryingToQuit = true;
        fadeOut.isFading = true;
    }

    private void Update()
    {
        if (tryingToPlay && fadeOut.finishedFading)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (tryingToDemoDialogue && fadeOut.finishedFading)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        if (tryingToQuit && fadeOut.finishedFading)
        {
            Application.Quit();
        }
    }
}
