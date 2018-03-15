﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Scoreboard scoreboard;
    public Transform startTransform;
    public int rnearCount = 0;
    public int bnearCount = 0;
    public AudioClip ballHittingGround;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (GM.currentState == GameState.PLAYING)
        {
            audioSource.clip = ballHittingGround;
            audioSource.Play();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal2")
        {
            scoreboard.redScore++;
            scoreboard.updateScore();
            resetTransform();
        }
        else if (other.tag == "Goal1")
        {
            scoreboard.blueScore++;
            scoreboard.updateScore();
            resetTransform();
        }
    }

    private void resetTransform() {
        transform.position = startTransform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void Update()
    {
        if (GM.currentState == GameState.PLAYING)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8);
            rnearCount = 0;
            bnearCount = 0;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "red")
                {
                    rnearCount++;
                }
                else if (hitColliders[i].tag == "blue")
                {
                    bnearCount++;
                }
            }
        }
    }
}