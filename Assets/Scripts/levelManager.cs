using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {

    public float myTimer = 180.0f;
    public Text timerText;

    public GameObject gameManager;

    // Use this for initialization
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        myTimer -= Time.deltaTime;
        timerText.text = myTimer.ToString("f2");

        if (myTimer <= 0.05f)
        {
            ++scoreRoundCounter.roundCount;
            if (scoreRoundCounter.roundCount < 3)
                SceneManager.LoadScene(1);

            else
            {
                Debug.Log("Load End Game Screen");
                scoreRoundCounter.roundCount = 0;
                scoreRoundCounter.player1Score = 0;
                scoreRoundCounter.player2Score = 0;
                scoreRoundCounter.player3Score = 0;
                scoreRoundCounter.player4Score = 0;
                SceneManager.LoadScene(2);
            }
        }
    }
}
