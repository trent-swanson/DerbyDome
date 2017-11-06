using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {
    public Time time;
    public float myTimer = 180.0f;
    public Text timerText;

    public GameObject gameManager;

    // Use this for initialization
    void Start(){}

    // Update is called once per frame
    void FixedUpdate()
    {
        myTimer -= Time.deltaTime;

        int min = Mathf.FloorToInt(myTimer / 60);
        int sec = Mathf.FloorToInt(myTimer % 60);
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");


        if (myTimer <= 0.05f)
        {
            ++Score.roundCount;
            if (Score.roundCount < 3)
                SceneManager.LoadScene(1);

            else
            {
                Debug.Log("Load End Game Screen");
                Score.roundCount = 0;
                SceneManager.LoadScene(2);
            }
        }
    }
}
