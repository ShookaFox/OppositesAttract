using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform ball;
    public float kickCooldown = 0.75f;
    private float timeToCoolDown = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float distanceToBall = Vector3.Distance(ball.transform.position, transform.position);
        Vector3 vectorToKick = ball.transform.position - transform.position;
        vectorToKick = vectorToKick.normalized;
        vectorToKick.y += 0.6f;

        if (distanceToBall <= 1.6f && timeToCoolDown <= 0)
        {
            
            if (Input.GetButtonDown("Fire1"))
            {
                ball.GetComponent<Rigidbody>().AddForce(vectorToKick * 100);
                timeToCoolDown = kickCooldown;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                ball.GetComponent<Rigidbody>().AddForce(vectorToKick * 175);
                timeToCoolDown = kickCooldown;
            }
        }
        timeToCoolDown -= Time.deltaTime;
	}
}
