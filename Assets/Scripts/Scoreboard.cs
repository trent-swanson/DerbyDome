using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using XInputDotNetPure;

public class Scoreboard : MonoBehaviour {
    
    static public Game_Manager.PlayerData[] gameLeaderboard = new Game_Manager.PlayerData[4];

    public Text[] names;
    public Text[] kills;
    public Text[] deaths;
    public Text[] scores;

    private float timer;
    public float wait = 10.0f;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        
        //gameLeaderboard = Game_Manager.playerData;
        System.Array.Copy(Game_Manager.playerData, gameLeaderboard, 4);
        for (int i = 0; i < gameLeaderboard.Length; i++)
        {
            for(int j = 0; j < gameLeaderboard.Length - 1; ++j)
            {
                if(gameLeaderboard[j].playerScore < gameLeaderboard[j + 1].playerScore)
                {
                    Game_Manager.PlayerData temp = gameLeaderboard[j];
                    gameLeaderboard[j] = gameLeaderboard[j + 1];
                    gameLeaderboard[j + 1] = temp;
                }
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
        if (timer >= wait || XCI.GetButtonDown(XboxButton.Start, XboxController.All) || XCI.GetButtonDown(XboxButton.B, XboxController.All))
        {
            Game_Manager.playerData[0].playerScore = 0;
            Game_Manager.playerData[1].playerScore = 0;
            Game_Manager.playerData[2].playerScore = 0;
            Game_Manager.playerData[3].playerScore = 0;

            Game_Manager.playerData[0].playerKills = 0;
            Game_Manager.playerData[1].playerKills = 0;
            Game_Manager.playerData[2].playerKills = 0;
            Game_Manager.playerData[3].playerKills = 0;

            Game_Manager.playerData[0].playerDeaths = 0;
            Game_Manager.playerData[1].playerDeaths = 0;
            Game_Manager.playerData[2].playerDeaths = 0;
            Game_Manager.playerData[3].playerDeaths = 0;

            Game_Manager.roundCount = 0;
            Game_Manager.SortLeaderBoard();

            SceneManager.LoadScene(0);
        }
	}
}
