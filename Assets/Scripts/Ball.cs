using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Scoreboard scoreboard;
    public Transform startTransform;
    public int rnearCount = 0;
    public int bnearCount = 0;
    public AudioClip ballHittingGround;

    private AudioSource audioSource;

    public GameObject[] redTeam;
    public GameObject[] blueTeam;

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
            rnearCount = 0;
            bnearCount = 0;
            
            foreach (GameObject rplayer in redTeam)
            {
                if (Vector3.Distance(transform.position, rplayer.transform.position) <= 8)
                {
                    rnearCount++;
                }
            }

            foreach (GameObject bplayer in blueTeam)
            {
                if (Vector3.Distance(transform.position, bplayer.transform.position) <= 8)
                {
                    bnearCount++;
                }
            }

            if ((transform.position.x < -68 || transform.position.x > 68) && (transform.position.z < -4 || transform.position.z > 4)) resetTransform();
        }
    }
}
