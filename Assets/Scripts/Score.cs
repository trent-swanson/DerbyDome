using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    static public int roundCount;
    public static int player1Score = 0;
    public static int player2Score = 0;
    public static int player3Score = 0;
    public static int player4Score = 0;

    [Space]
    public static int player1Kills = 0;
    public static int player2Kills = 0;
    public static int player3Kills = 0;
    public static int player4Kills = 0;

    [Space]
    public static int player1Deaths = 0;
    public static int player2Deaths = 0;
    public static int player3Deaths = 0;
    public static int player4Deaths = 0;
    
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text Player3ScoreText;
    public Text Player4ScoreText;

    void Start()
    {
        Player1ScoreText.text = "Player 1: " + player1Score.ToString();
        Player2ScoreText.text = "Player 2: " + player2Score.ToString();
        Player3ScoreText.text = "Player 3: " + player3Score.ToString();
        Player4ScoreText.text = "Player 4: " + player4Score.ToString();
    }

    public void ScoreIncrease(int playerID, int score)
    {
        if(playerID == 1) {
            player1Score += score;
            Player1ScoreText.text = "Player 1: " + player1Score.ToString();
        } else if(playerID == 2) {
            player2Score += score;
            Player2ScoreText.text = "Player 2: " + player2Score.ToString();
        } else if(playerID == 3) {
            player3Score += score;
            Player3ScoreText.text = "Player 3: " + player3Score.ToString();
        } else if(playerID == 4) {
            player4Score += score;
            Player4ScoreText.text = "Player 4: " + player4Score.ToString();
        }
    }

    public void killIncrease(int playerID)
    {
        if(playerID == 1) {
            player1Kills++;
        } else if(playerID == 2) {
            player2Kills++;
        } else if(playerID == 3) {
            player3Kills++;
        } else if(playerID == 4) {
            player4Kills++;
        }
    }
}
