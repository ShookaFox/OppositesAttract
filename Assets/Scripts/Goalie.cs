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
        agent.SetDestination(new Vector3(Mathf.Clamp(((ball.transform.position.x + 32f)/64f)*4, 0, 4) + startingPos.x, transform.position.y, Mathf.Clamp(ball.transform.position.z, -6, 6)));
        if (Vector3.Distance(ball.transform.position, transform.position) <= 1)
        {
            if (Vector3.Angle(ballToGoal, this.transform.forward) > 50)
            {
                Vector3 kickVector = this.transform.forward;
                kickVector = kickVector.normalized;
                ball.GetComponent<Rigidbody>().AddForce(kickVector * 30);
            }
            else
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(ballToGoal), Time.deltaTime * agent.angularSpeed);
            }
        }
    }
}
