using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour {
    public TextMeshPro scoreboardText;
    public TextMeshPro timerText;
    public int blueScore = 0;
    public int redScore = 0;

    public float timer = 0f;
    bool timerRunning = false;

    public void updateScore()
    {
        scoreboardText.text = blueScore + " - " + redScore;
    }

    private void Update()
    {
        if (timerRunning)
        {
            timer += Time.deltaTime;
            timerText.text = (Mathf.FloorToInt(timer / 60f)) + ":" + (Mathf.FloorToInt(((timer / 60f) - Mathf.FloorToInt(timer / 60f)) * 60f)).ToString("D2");
            if (timer >= 60 * 3)
            {
                Debug.Log(GM.instance);
                GM.instance.OnGameEnded(redScore, blueScore);
                timerRunning = false;
            }
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}
