//================================================================================
//Score
//
//Purpose: To control the players movement, store the players health
//and determine when the player is dead
//
//Creator: Trent Swanson
//Edited by: Joel Goodchild
//================================================================================

using UnityEngine;

public class Score : MonoBehaviour {
    
    public delegate void UpdatePlayerLeaderAction();
    public static event UpdatePlayerLeaderAction OnUpdatePlayerLeader;
    public delegate void LateUpdatePlayerLeaderAction();
    public static event LateUpdatePlayerLeaderAction OnLateUpdatePlayerLeader;
    
    [Space]

    public LeaderBoard[] iconLeaderBoards;
    public bool debugControls = false;

    void Start()
    {
        UpdateLeaderBoard();
    }

    public void ScoreIncrease(int playerID, int score)
    {
        Game_Manager.isDraw = false;
        Game_Manager.playerData[playerID - 1].playerScore += score;
        UpdateLeaderBoard();
    }

    public void killIncrease(int playerID)
    {
        Game_Manager.playerData[playerID - 1].playerKills++;
    }

    public void UpdateLeaderBoard()
    {
        Game_Manager.SortLeaderBoard();
        iconLeaderBoards[0].UpdateLeaderBoard(Game_Manager.leaderboard);
        iconLeaderBoards[1].UpdateLeaderBoard(Game_Manager.leaderboard);
        iconLeaderBoards[2].UpdateLeaderBoard(Game_Manager.leaderboard);
        iconLeaderBoards[3].UpdateLeaderBoard(Game_Manager.leaderboard);
        if(OnUpdatePlayerLeader != null)
                OnUpdatePlayerLeader();
                OnLateUpdatePlayerLeader();
    }

    void Update()
    {
        //Checks if the game is in debug mode, if it is, then the debug controls are enabled
        if (debugControls)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                ScoreIncrease(1, 100);

            if (Input.GetKeyDown(KeyCode.W))
                ScoreIncrease(2, 100);

            if (Input.GetKeyDown(KeyCode.E))
                ScoreIncrease(3, 100);

            if (Input.GetKeyDown(KeyCode.R))
                ScoreIncrease(4, 100);
        }
    }
}
