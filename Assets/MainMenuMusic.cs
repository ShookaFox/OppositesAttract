using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour {

    public AudioClip music;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.Play();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
