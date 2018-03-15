using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour {
    public TextMeshPro scoreboardText;
    public int blueScore = 0;
    public int redScore = 0;

    public void updateScore()
    {
        scoreboardText.text = blueScore + " - " + redScore;
    }
}
