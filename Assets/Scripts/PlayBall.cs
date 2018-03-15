using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayBall : MonoBehaviour
{
    public bool allowedToChase;
    private Vector3 startingPos;
    NavMeshAgent agent;
    public Transform ball;
    public Transform myGoal;
    public int myTeam;

	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(3.0f, 7.0f);
        if (Random.Range(0, 7) == 4) agent.speed = Random.Range(7.0f, 17.0f);
        startingPos = transform.position;
    }
	
	void Update () {
        if (GM.currentState == GameState.PLAYING)
        {
            Vector3 ballToGoal = myGoal.position - ball.position;

            Vector3 desiredDestination = ball.position - ballToGoal.normalized;

            float distanceToBall = Vector3.Distance(ball.transform.position, transform.position);

            agent.SetDestination(desiredDestination);
            if (distanceToBall <= 1.5f)
            {
                if (Vector3.Angle(ballToGoal, this.transform.forward) < 10)
                {
                    ball.GetComponent<Rigidbody>().AddForce(this.transform.forward * 20);
                }
                else
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(ballToGoal), Time.deltaTime * agent.angularSpeed);
                }
            }
        }
	}
}
