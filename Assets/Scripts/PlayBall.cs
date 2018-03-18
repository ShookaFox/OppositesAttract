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

    public float kickLvl1 = 20f;
    public float kickLvl2 = 40f;
    public float kickLvl3 = 75f;

    void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(7.0f, 15.0f);
        startingPos = transform.position;
    }
	
	void Update () {
        if (GM.currentState == GameState.PLAYING)
        {
            Vector3 ballToGoal = myGoal.position - ball.position;

            Vector3 desiredDestination = ball.position - ballToGoal.normalized;

            float distanceToBall = Vector3.Distance(ball.transform.position, transform.position);

            if ((ball.GetComponent<Ball>().rnearCount < 3 && myTeam == 1) || (ball.GetComponent<Ball>().bnearCount < 3 && myTeam == 2) || Vector3.Distance(transform.position, ball.position) < 2)
            {
                agent.SetDestination(desiredDestination);
            }
            else
            {
                agent.SetDestination(startingPos);
            }
            
            if (distanceToBall <= 1.5f)
            {
                Vector3 vectorToKick = this.transform.forward;
                vectorToKick.y += 0.15f;
                float angle = Vector3.Angle(ballToGoal, this.transform.forward);
                if (angle < 10)
                {
                    ball.GetComponent<Rigidbody>().AddForce(vectorToKick * kickLvl3);
                }
                else if (angle < 20)
                {
                    ball.GetComponent<Rigidbody>().AddForce(vectorToKick * kickLvl2);
                }
                else if (angle < 50)
                {
                    ball.GetComponent<Rigidbody>().AddForce(vectorToKick * kickLvl1);
                }
                else
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(ballToGoal), Time.deltaTime * agent.angularSpeed);
                }
            }
        }
	}
}
