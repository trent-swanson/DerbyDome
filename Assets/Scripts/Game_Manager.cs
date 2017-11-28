//================================================================================
//Game_Manager
//
//Purpose: Creates and stores all player stats
//
//Creator: Trent Swanson
//================================================================================

using UnityEngine;

public class Game_Manager : MonoBehaviour
{
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
    static public bool isDraw = true;

    static public int roundCount;

	private static bool spawned = false;

    void Awake()
    {
        if(spawned == false)
        {
        	spawned = true;
            DontDestroyOnLoad(gameObject);
			playerData[0] = new PlayerData("RED", 1, 0, 0, 0);
			playerData[1] = new PlayerData("BLUE", 2, 0, 0, 0);
			playerData[2] = new PlayerData("WHITE", 3, 0, 0, 0);
			playerData[3] = new PlayerData("PINK", 4, 0, 0, 0);
        }

    	else
        	DestroyImmediate(gameObject);
    }

    static public PlayerData[] SortLeaderBoardReturn()
    {
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
