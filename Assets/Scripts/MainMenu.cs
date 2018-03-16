using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public FadeOut fadeOut;
    public MainMenuMusic mainMenuMusic;
    public AudioSource audioSource;

    private bool tryingToPlay = false;
    private bool tryingToQuit = false;
    private bool tryingToDemoDialogue = false;

    public AudioClip clickSound;

    private void Start()
    {
        mainMenuMusic = FindObjectOfType<MainMenuMusic>();
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
            AudioManager.instance.pauseMainMenuMusic();
            SceneManager.LoadScene(3);
        }
        if (tryingToDemoDialogue && fadeOut.finishedFading)
        {
            AudioManager.instance.pauseMainMenuMusic();
            SceneManager.LoadScene(3);
        }
        if (tryingToQuit && fadeOut.finishedFading)
        {
            AudioManager.instance.pauseMainMenuMusic();
            Application.Quit();
        }
    }
}
