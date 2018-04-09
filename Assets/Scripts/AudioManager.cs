using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioSource musicPlayer;
    public AudioSource soundPlayer;
    
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    public AudioClip music5;
    public int musicTrack = 0;
    public bool currentlyPlayingMusic = false;
    public AudioClip clickingSound;
    public AudioClip hoveredSound;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    public void playMusic()
    {
        switch (musicTrack)
        {
            case 1:
                musicPlayer.clip = music2;
                break;
            case 2:
                musicPlayer.clip = music3;
                break;
            case 3:
                musicPlayer.clip = music4;
                break;
            case 4:
                musicPlayer.clip = music5;
                break;
            case 0:
                musicPlayer.clip = music1;
                break;
        }
        musicPlayer.Play();
        currentlyPlayingMusic = true;
    }

    private void Update()
    {
        if (currentlyPlayingMusic && musicPlayer.time >= musicPlayer.clip.length-0.1f)
        {
            musicTrack++;
            switch (musicTrack)
            {
                case 1:
                    musicPlayer.clip = music2;
                    break;
                case 2:
                    musicPlayer.clip = music3;
                    break;
                case 3:
                    musicPlayer.clip = music4;
                    break;
                case 4:
                    musicPlayer.clip = music5;
                    break;
                case 5:
                    musicTrack = 0;
                    musicPlayer.clip = music1;
                    break;
            }
            musicPlayer.time = 0;
            musicPlayer.Play();
        }
    }

    public void pauseMainMenuMusic()
    {
        musicPlayer.Pause();
    }

    public void playButtonClicked()
    {
        soundPlayer.clip = clickingSound;
        soundPlayer.Play();
    }

    public void playButtonHovered()
    {
        soundPlayer.clip = hoveredSound;
        soundPlayer.Play();
    }
}
