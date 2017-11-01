using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {

    private int[] score = new int[4];
    private int[] playerNum = new int[4];
    public Text[] leaderboard;
    private float timer;
    public float wait = 10.0f;

	// Use this for initialization
	void Start ()
    {
        score[0] = Score.Player1Score;
        score[1] = Score.Player2Score;
        score[2] = Score.Player3Score;
        score[3] = Score.Player4Score;
        playerNum[0] = 1;
        playerNum[1] = 2;
        playerNum[2] = 3;
        playerNum[3] = 4;

        for (int i = 0; i < score.Length - 1; ++i)
        {
            for(int j = 0; j < score.Length - 1; ++j)
            {
                if(score[j] < score[j + 1])
                {
                    int temp = score[j];
                    score[j] = score[j + 1];
                    score[j + 1] = temp;

                    int temp2 = playerNum[j];
                    playerNum[j] = playerNum[j + 1];
                    playerNum[j + 1] = temp2;
                }
            }
        }

        for(int x = 0; x < score.Length; ++x)
        {
            leaderboard[x].text = "Player " + playerNum[x] + ": " + score[x];
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= wait)
        {
            SceneManager.LoadScene(0);
            Score.Player1Score = 0;
            Score.Player2Score = 0;
            Score.Player3Score = 0;
            Score.Player4Score = 0;
            scoreRoundCounter.roundCount = 0;
        }
	}
}
