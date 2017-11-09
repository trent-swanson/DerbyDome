using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    
    public delegate void UpdatePlayerIconsAction();
    public static event UpdatePlayerIconsAction OnUpdatePlayerIcons;
    
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text Player3ScoreText;
    public Text Player4ScoreText;

    [Space]
    public LeaderBoard[] iconLeaderBoards;

    private int[] tempScores = new int[4];
    private int[] tempID = new int[4];

    void Start()
    {
        Player1ScoreText.text = "Score: " + Game_Manager.playerData[0].playerScore.ToString();
        Player2ScoreText.text = "Score: " + Game_Manager.playerData[1].playerScore.ToString();
        Player3ScoreText.text = "Score: " + Game_Manager.playerData[2].playerScore.ToString();
        Player4ScoreText.text = "Score: " + Game_Manager.playerData[3].playerScore.ToString();
    }

    public void ScoreIncrease(int playerID, int score)
    {
        if(playerID == 1) {
            Game_Manager.playerData[0].playerScore += score;
            Player1ScoreText.text = "Score: " + Game_Manager.playerData[0].playerScore.ToString();
        } else if(playerID == 2) {
            Game_Manager.playerData[1].playerScore += score;
            Player2ScoreText.text = "Score: " + Game_Manager.playerData[1].playerScore.ToString();
        } else if(playerID == 3) {
            Game_Manager.playerData[2].playerScore += score;
            Player3ScoreText.text = "Score: " + Game_Manager.playerData[2].playerScore.ToString();
        } else if(playerID == 4) {
            Game_Manager.playerData[3].playerScore += score;
            Player4ScoreText.text = "Score: " + Game_Manager.playerData[3].playerScore.ToString();
        }
        UpdateLeaderBoard();
    }

    public void killIncrease(int playerID)
    {
        if(playerID == 1) {
            Game_Manager.playerData[0].playerKills++;
        } else if(playerID == 2) {
            Game_Manager.playerData[1].playerKills++;
        } else if(playerID == 3) {
            Game_Manager.playerData[2].playerKills++;
        } else if(playerID == 4) {
            Game_Manager.playerData[3].playerKills++;
        }
    }

    public void UpdateLeaderBoard()
    {
        Game_Manager.SortLeaderBoard();
        iconLeaderBoards[0].UpdateLeaderBoard(Game_Manager.leaderboard);
        iconLeaderBoards[1].UpdateLeaderBoard(Game_Manager.leaderboard);
        iconLeaderBoards[2].UpdateLeaderBoard(Game_Manager.leaderboard);
        iconLeaderBoards[3].UpdateLeaderBoard(Game_Manager.leaderboard);
        if(OnUpdatePlayerIcons != null)
                OnUpdatePlayerIcons();
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
