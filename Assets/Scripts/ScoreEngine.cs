using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEngine : MonoBehaviour {
	public static int redScore = 0, blueScore = 0;
	void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 20), "Red Team" + redScore);
		GUI.Label(new Rect(Screen.width - 100 + 10, 10, 100, 20), "Blue Team" + blueScore);
    }
}