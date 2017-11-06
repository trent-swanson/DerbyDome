using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {
    
    static public Score.PlayerData[] gameLeaderboard = new Score.PlayerData[4];

    public Text[] names;
    public Text[] kills;
    public Text[] deaths;
    public Text[] scores;

    private float timer;
    public float wait = 10.0f;

	// Use this for initialization
	void Start ()
    {
        gameLeaderboard = Score.playerData;
        for(int j = 0; j < gameLeaderboard.Length - 1; ++j)
        {
            if(gameLeaderboard[j].playerScore < gameLeaderboard[j + 1].playerScore)
            {
                Score.PlayerData temp = gameLeaderboard[j];
                gameLeaderboard[j] = gameLeaderboard[j + 1];
                gameLeaderboard[j + 1] = temp;
            }
        }

        for(int x = 0; x < gameLeaderboard.Length; ++x)
        {
            names[x].text = gameLeaderboard[x].playerName;
            kills[x].text = "K[" + gameLeaderboard[x].playerKills.ToString() + "]";
            deaths[x].text = "D[" + gameLeaderboard[x].playerDeaths.ToString() + "]";
            scores[x].text = gameLeaderboard[x].playerScore.ToString();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= wait)
        {
            Score.playerData[0].playerScore = 0;
            Score.playerData[1].playerScore = 0;
            Score.playerData[2].playerScore = 0;
            Score.playerData[3].playerScore = 0;

            Score.playerData[0].playerKills = 0;
            Score.playerData[1].playerKills = 0;
            Score.playerData[2].playerKills = 0;
            Score.playerData[3].playerKills = 0;

            Score.playerData[0].playerDeaths = 0;
            Score.playerData[1].playerDeaths = 0;
            Score.playerData[2].playerDeaths = 0;
            Score.playerData[3].playerDeaths = 0;

            Score.roundCount = 0;

            SceneManager.LoadScene(0);
        }
	}
}
