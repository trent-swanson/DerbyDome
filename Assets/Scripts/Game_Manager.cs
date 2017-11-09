using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

	public struct PlayerData {
        public int playerID;
        public string playerName;
        public int playerKills;
        public int playerDeaths;
        public int playerScore;

        public PlayerData(string _name, int _id, int _kills, int _deaths, int _score)
        {
            playerName = _name;
            playerID = _id;
            playerKills = _kills;
            playerDeaths = _deaths;
            playerScore = _score;
        }
    }

    static public PlayerData[] playerData = new PlayerData[4];
    static public PlayerData[] leaderboard = new PlayerData[4];

    static public int roundCount;

	private static bool spawned = false;

    void Awake()
    {
        if(spawned == false)
        {
        	spawned = true;
            DontDestroyOnLoad(gameObject);
			playerData[0] = new PlayerData("Player One", 1, 0, 0, 0);
			playerData[1] = new PlayerData("Player Two", 2, 0, 0, 0);
			playerData[2] = new PlayerData("Player Three", 3, 0, 0, 0);
			playerData[3] = new PlayerData("Player Four", 4, 0, 0, 0);
        }
    	else
        {
        	DestroyImmediate(gameObject);
        }
    }

	static public PlayerData[] SortLeaderBoardReturn()
    {
        //leaderboard = playerData;
		System.Array.Copy(playerData, leaderboard, 4);
        for (int i = 0; i < leaderboard.Length; i++)
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
		return leaderboard;
    }

	static public void SortLeaderBoard()
    {
        //leaderboard = playerData;
		System.Array.Copy(playerData, leaderboard, 4);
        for (int i = 0; i < leaderboard.Length; i++)
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
    }
}
