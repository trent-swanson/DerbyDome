using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public class PlayerData {
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
        playerData[0] = new PlayerData("Player One", 1, 0, 0, 0);
        playerData[1] = new PlayerData("Player Two", 2, 0, 0, 0);
        playerData[2] = new PlayerData("Player Three", 3, 0, 0, 0);
        playerData[3] = new PlayerData("Player Four", 4, 0, 0, 0);

        leaderboard[0] = new PlayerData("Player One", 1, 0, 0, 0);
        leaderboard[1] = new PlayerData("Player Two", 2, 0, 0, 0);
        leaderboard[2] = new PlayerData("Player Three", 3, 0, 0, 0);
        leaderboard[3] = new PlayerData("Player Four", 4, 0, 0, 0);

        Player1ScoreText.text = "Score: " + playerData[0].playerScore.ToString();
        Player2ScoreText.text = "Score: " + playerData[1].playerScore.ToString();
        Player3ScoreText.text = "Score: " + playerData[2].playerScore.ToString();
        Player4ScoreText.text = "Score: " + playerData[3].playerScore.ToString();
    }

    public void ScoreIncrease(int playerID, int score)
    {
        if(playerID == 1) {
            playerData[0].playerScore += score;
            Player1ScoreText.text = "Score: " + playerData[0].playerScore.ToString();
        } else if(playerID == 2) {
            playerData[1].playerScore += score;
            Player2ScoreText.text = "Score: " + playerData[1].playerScore.ToString();
        } else if(playerID == 3) {
            playerData[2].playerScore += score;
            Player3ScoreText.text = "Score: " + playerData[2].playerScore.ToString();
        } else if(playerID == 4) {
            playerData[3].playerScore += score;
            Player4ScoreText.text = "Score: " + playerData[3].playerScore.ToString();
        }
    }

    public void killIncrease(int playerID)
    {
        if(playerID == 1) {
            playerData[0].playerKills++;
        } else if(playerID == 2) {
            playerData[1].playerKills++;
        } else if(playerID == 3) {
            playerData[2].playerKills++;
        } else if(playerID == 4) {
            playerData[3].playerKills++;
        }
    }

    public void SortLeaderBoard()
    {
        leaderboard = playerData;
        for(int j = 0; j < leaderboard.Length - 1; ++j)
        {
            if(leaderboard[j].playerScore < leaderboard[j + 1].playerScore)
            {
                PlayerData temp = leaderboard[j];
                leaderboard[j] = leaderboard[j + 1];
                leaderboard[j + 1] = temp;
            }
        }

        iconLeaderBoards[0].UpdateLeaderBoard(leaderboard);
        iconLeaderBoards[1].UpdateLeaderBoard(leaderboard);
        iconLeaderBoards[2].UpdateLeaderBoard(leaderboard);
        iconLeaderBoards[3].UpdateLeaderBoard(leaderboard);
    }
}
