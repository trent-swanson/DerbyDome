using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {

    public float myTimer = 180.0f;
    public Text timerText;

    public GameObject gameManager;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("roundCount" + scoreRoundCounter.roundCount);
    }

    // Update is called once per frame
    void Update()
    {
        myTimer -= Time.deltaTime;
        timerText.text = myTimer.ToString("f2");

        if (myTimer <= 0.05f)
        {
            ++scoreRoundCounter.roundCount;
            if (scoreRoundCounter.roundCount < 3)
                SceneManager.LoadScene(0);

            else
            {
                Debug.Log("Load End Game Screen");
                scoreRoundCounter.roundCount = 0;
                scoreRoundCounter.player1Score = 0;
                scoreRoundCounter.player2Score = 0;
                scoreRoundCounter.player3Score = 0;
                scoreRoundCounter.player4Score = 0;
                SceneManager.LoadScene(0);
            }
        }
    }
}
