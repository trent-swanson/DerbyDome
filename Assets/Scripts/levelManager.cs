using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {
    public Time time;
    public float myTimer = 180.0f;
    public Text timerText;
    public GameObject gameManager;
    [Space]
    public Text player1Round;
    public Text player2Round;
    public Text player3Round;
    public Text player4Round;
    [Space]
    public Text player1Timer;
    public Text player2Timer;
    public Text player3Timer;
    public Text player4Timer;

    private bool roundStart = true;
    private float startSeconds = 3.5f;
    private float three = 3.0f;

    public AudioSource AudioSource;
    public AudioSource AudioSource2;
    public AudioClip countDownClip;
    public AudioClip musicClip1;
    public AudioClip musicClip2;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        PlayLevelAudio();
    }

    void PlayLevelAudio()
    {
        AudioSource.PlayOneShot(countDownClip, 0.35f);
        if (Game_Manager.roundCount == 0)
            AudioSource2.PlayOneShot(musicClip1, 0.2f);
        if (Game_Manager.roundCount == 1)
            AudioSource2.PlayOneShot(musicClip2, 0.2f);
        if (Game_Manager.roundCount == 2)
            AudioSource2.PlayOneShot(musicClip1, 0.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (roundStart)
        {
            //Removes the ability for players to control their cars during the countdown timer
            CarController.canControl = false;
            //Counts down from 3 and sets each associated text element to the timer
            startSeconds -= Time.deltaTime;
            if (Game_Manager.roundCount == 0) {
                player1Round.text = ("Round One");
                player2Round.text = ("Round One");
                player3Round.text = ("Round One");
                player4Round.text = ("Round One");
            } else if (Game_Manager.roundCount == 1) {
                player1Round.text = ("Round Two");
                player2Round.text = ("Round Two");
                player3Round.text = ("Round Two");
                player4Round.text = ("Round Two");
            } else if (Game_Manager.roundCount == 2) {
                player1Round.text = ("Round Three");
                player2Round.text = ("Round Three");
                player3Round.text = ("Round Three");
                player4Round.text = ("Round Three");
            }


            //As the timer starts above 3 to get a good feeling, it ensures that the timer never displays a value above 3
            if (startSeconds > 3.0f)
            {
                timerText.text = three.ToString("0");
                player1Timer.text = three.ToString("0");
                player2Timer.text = three.ToString("0");
                player3Timer.text = three.ToString("0");
                player4Timer.text = three.ToString("0");
            }
            //If the timer goes below 3, it starts counting down normally
            else
            {
                timerText.text = startSeconds.ToString("0");
                player1Timer.text = startSeconds.ToString("0");
                player2Timer.text = startSeconds.ToString("0");
                player3Timer.text = startSeconds.ToString("0");
                player4Timer.text = startSeconds.ToString("0");
            }

            //When the timer gets close to 0, but doesn't display zero, the text tells the player to GO!
            if (startSeconds <= 0.5f)
            {
                timerText.text = ("GO!");
                player1Timer.text = ("GO!");
                player2Timer.text = ("GO!");
                player3Timer.text = ("GO!");
                player4Timer.text = ("GO!");
            }

            //Once the GO! hangs for a second, the players gain control of their car, and the round starts
            if (startSeconds <= -0.5f)
            {
                roundStart = false;
                player1Timer.text = null;
                player2Timer.text = null;
                player3Timer.text = null;
                player4Timer.text = null;

                player1Round.text = null;
                player2Round.text = null;
                player3Round.text = null;
                player4Round.text = null;

            }
        }

        //Once the countdown timer has finished, the round timer commences
        if (!roundStart)
        {
            CarController.canControl = true;
            myTimer -= Time.deltaTime;

            int min = Mathf.FloorToInt(myTimer / 60);
            int sec = Mathf.FloorToInt(myTimer % 60);
            timerText.text = min.ToString("00") + ":" + sec.ToString("00");


            if (myTimer <= 0.05f)
            {
                ++Game_Manager.roundCount;
                roundStart = true;
                if (Game_Manager.roundCount < 3)
                    SceneManager.LoadScene(1);

                else
                {
                    Debug.Log("Load End Game Screen");
                    SceneManager.LoadScene(2);
                }
            }     
        }
    }
}
