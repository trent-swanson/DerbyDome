using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int Player1ScoreActual;
    public static int Player2ScoreActual;
    public static int Player3ScoreActual;
    public static int Player4ScoreActual;
    
    public Text Player1Score;
    public Text Player2Score;
    public Text Player3Score;
    public Text Player4Score;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (Player1Score.gameObject.tag == "PlayerScore 1")
        {
            Player1Score.text = "Player 1:" + Player1ScoreActual.ToString();
        }
        if (Player2Score.gameObject.tag == "PlayerScore 2")
        {
            Player2Score.text = "Player 2:" + Player2ScoreActual.ToString();
        }
        if (Player3Score.gameObject.tag == "PlayerScore 3")
        {
            Player3Score.text = "Player 3:" + Player3ScoreActual.ToString();
        }
        if (Player4Score.gameObject.tag == "PlayerScore 4")
        {
            Player4Score.text = "Player 4:" + Player4ScoreActual.ToString();
        }
    }
}
