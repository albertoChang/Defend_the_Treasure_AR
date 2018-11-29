﻿using UnityEngine;
using TMPro;

public class GameTimerScript : MonoBehaviour {

    TextMeshProUGUI gameTimerText;
    float gameTimer = 91f;
    private bool gameActive;
    public GameObject WonPanel;

    void Start()
    {
        gameTimerText = GetComponent<TextMeshProUGUI>();
    }

    public void activateGame()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update () {
        if (gameActive)
        {
            gameTimer -= Time.deltaTime;
            int seconds = (int)(gameTimer % 60);
            int minutes = (int)(gameTimer / 60) % 60;

            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
            gameTimerText.text = timeString;
        }
        
        if ( gameTimer < 0f)
        {
            gameTimer = 0f;
            gameActive = false;
            GameManager.Won(WonPanel);
        }
	}
}
