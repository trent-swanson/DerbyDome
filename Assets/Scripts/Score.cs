using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    
    public delegate void UpdatePlayerLeaderAction();
    public static event UpdatePlayerLeaderAction OnUpdatePlayerLeader;

    public delegate void LateUpdatePlayerLeaderAction();
    public static event LateUpdatePlayerLeaderAction OnLateUpdatePlayerLeader;
    
    //public Text Player1ScoreText;
    //public Text Player2ScoreText;
    //public Text Player3ScoreText;
    //public Text Player4ScoreText;

    [Space]
    public LeaderBoard[] iconLeaderBoards;

    private int[] tempScores = new int[4];
    private int[] tempID = new int[4];

    void Start()
    {
        //Player1ScoreText.text = "Score: " + Game_Manager.playerData[0].playerScore.ToString();
        //Player2ScoreText.text = "Score: " + Game_Manager.playerData[1].playerScore.ToString();
        //Player3ScoreText.text = "Score: " + Game_Manager.playerData[2].playerScore.ToString();
        //Player4ScoreText.text = "Score: " + Game_Manager.playerData[3].playerScore.ToString();
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
        if(Input.GetKeyDown(KeyCode.Q)) {
            ScoreIncrease(1, 100);
        }
        if(Input.GetKeyDown(KeyCode.W)) {
            ScoreIncrease(2, 100);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            ScoreIncrease(3, 100);
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            ScoreIncrease(4, 100);
        }
    }
}
