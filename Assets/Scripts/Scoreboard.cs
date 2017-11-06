using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {

    public struct PlayerData {
        public string playerName;
        public int playerKills;
        public int playerDeaths;
        public int playerScore;
    }

    private PlayerData[] leaderboard = new PlayerData[4];
    //private int[] score = new int[4];
    //private int[] playerNum = new int[4];
    
    public Text[] names;
    public Text[] kills;
    public Text[] deaths;
    public Text[] scores;

    private float timer;
    public float wait = 10.0f;

	// Use this for initialization
	void Start ()
    {
        leaderboard[0].playerName = "Player One";
        leaderboard[1].playerName = "Player Two";
        leaderboard[2].playerName = "Player Three";
        leaderboard[3].playerName = "Player Four";

        leaderboard[0].playerKills = Score.player1Kills;
        leaderboard[1].playerKills = Score.player2Kills;
        leaderboard[2].playerKills = Score.player3Kills;
        leaderboard[3].playerKills = Score.player4Kills;

        leaderboard[0].playerDeaths = Score.player1Deaths;
        leaderboard[1].playerDeaths = Score.player2Deaths;
        leaderboard[2].playerDeaths = Score.player3Deaths;
        leaderboard[3].playerDeaths = Score.player4Deaths;

        leaderboard[0].playerScore = Score.player1Score;
        leaderboard[1].playerScore = Score.player2Score;
        leaderboard[2].playerScore = Score.player3Score;
        leaderboard[3].playerScore = Score.player4Score;

        for (int i = 0; i < leaderboard.Length - 1; ++i)
        {
            for(int j = 0; j < leaderboard.Length - 1; ++j)
            {
                if(leaderboard[j].playerScore < leaderboard[j + 1].playerScore)
                {
                    PlayerData temp = leaderboard[j];
                    leaderboard[j] = leaderboard[j + 1];
                    leaderboard[j + 1] = temp;
                }
            }
        }

        for(int x = 0; x < leaderboard.Length; ++x)
        {
            names[x].text = leaderboard[x].playerName;
            kills[x].text = "K[" + leaderboard[x].playerKills.ToString() + "]";
            deaths[x].text = "D[" + leaderboard[x].playerDeaths.ToString() + "]";
            scores[x].text = leaderboard[x].playerScore.ToString();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= wait)
        {
            Score.player1Score = 0;
            Score.player2Score = 0;
            Score.player3Score = 0;
            Score.player4Score = 0;

            Score.player1Kills = 0;
            Score.player2Kills = 0;
            Score.player3Kills = 0;
            Score.player4Kills = 0;

            Score.player1Deaths = 0;
            Score.player2Deaths = 0;
            Score.player3Deaths = 0;
            Score.player4Deaths = 0;

            Score.roundCount = 0;

            SceneManager.LoadScene(0);
        }
	}
}
