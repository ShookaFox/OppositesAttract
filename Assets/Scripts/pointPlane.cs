using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointPlane : MonoBehaviour {
	public GoalType goal;
	void OnTriggerEnter(Collider other) {
		if(other.tag == "ball")
			if (goal == GoalType.RED)
				ScoreEngine.redScore++;
			else
				ScoreEngine.blueScore++;
	}
}
public enum GoalType {
	RED,
	BLUE
}