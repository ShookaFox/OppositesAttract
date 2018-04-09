using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public FadeOut fadeOut;
    public AudioSource audioSource;

    private bool tryingToQuit = false;

    public AudioClip clickSound;

    private void Start()
    {
        
    }

    public void PlayClickSound()
    {
        AudioManager.instance.playButtonClicked();
    }

    public void PlayHoveredSound()
    {
        AudioManager.instance.playButtonHovered();
    }

    public void PlayGame()
    {
        fadeOut.FadeOutNow(ActuallyPlayGame);
    }

    public void ActuallyPlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        tryingToQuit = true;
        fadeOut.isFading = true;
    }

    private void Update()
    {
        if (tryingToQuit && fadeOut.finishedFading)
        {
            Application.Quit();
        }
    }
}
