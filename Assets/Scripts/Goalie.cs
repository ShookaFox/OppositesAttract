using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goalie : AIPlayer {
    private Vector3 startingPos;
    public Transform otherGoal;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(5.0f, 15.0f);
        startingPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (GM.currentState == GameState.PLAYING)
        {
            //Ray ray = new Ray(ball.transform.position, ball.transform.forward);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 100, 8))
            //{
            //    if (hit.transform != myGoal.transform)
            //    {
            //        Debug.Log()

            //    }
            //}
            Vector3 ballToGoal = otherGoal.position - ball.position;
            Vector3 ballToMyGoal = myGoal.position - ball.position;
            Vector3 desiredDestination = ball.position - ballToMyGoal.normalized;
            Vector3 ballNotCloseDesiredPosition = new Vector3(Mathf.Clamp(((ball.transform.position.x + 32f) / 64f) * 4, 0, 7) + startingPos.x, transform.position.y, Mathf.Clamp(ball.transform.position.z, -10, 10));
            if (Vector3.Distance(ball.transform.position, transform.position) <= 4.5f && Vector3.Distance(transform.position, startingPos) < 10)
            {
                agent.SetDestination(desiredDestination);
            }
            else
            {
                agent.SetDestination(ballNotCloseDesiredPosition);
            }
            
            if (Vector3.Distance(ball.transform.position, transform.position) <= 1.5f)
            {
                Vector3 vectorToKick = ball.transform.position - transform.position;
                vectorToKick = vectorToKick.normalized;
                if (Vector3.Angle(ballToGoal, this.transform.forward) > 40)
                {
                    vectorToKick.y += 0.2f;

                    ball.GetComponent<Rigidbody>().AddForce(vectorToKick * 79);
                }
                else
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(ballToGoal), Time.deltaTime * agent.angularSpeed);
                }
            }
        }
    }
}
