using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int Player1Score = 0;
    public static int Player2Score = 0;
    public static int Player3Score = 0;
    public static int Player4Score = 0;
    
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text Player3ScoreText;
    public Text Player4ScoreText;

    public void ScoreIncrease(int playerID, int score)
    {
        if(playerID == 1) {
            Player1Score += score;
            Player1ScoreText.text = "Player 1: " + Player1Score.ToString();
        } else if(playerID == 2) {
            Player2Score += score;
            Player2ScoreText.text = "Player 2: " + Player2Score.ToString();
        } else if(playerID == 3) {
            Player3Score += score;
            Player3ScoreText.text = "Player 3: " + Player3Score.ToString();
        } else if(playerID == 4) {
            Player4Score += score;
            Player4ScoreText.text = "Player 4: " + Player4Score.ToString();
        }
    }
}
