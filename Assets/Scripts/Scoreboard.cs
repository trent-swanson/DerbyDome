//================================================================================
//Scoreboard
//
//Purpose: Controls the end game scoreboard and allows the car materials
//to be changed over
//
//Creator: Joel Goodchild
//Edited by: Ryan Ward
//Edited by: Trent Swanson
//================================================================================
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using XInputDotNetPure;

public class Scoreboard : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource crowdSource;
    public AudioClip music;
    public AudioClip crowd;
    
    static public Game_Manager.PlayerData[] gameLeaderboard = new Game_Manager.PlayerData[4];

    public Text[] names;
    public Text[] kills;
    public Text[] deaths;
    public Text[] scores;
    public Material[] carMaterials;
    public Color[] carColours;

    private float timer;
    public float wait = 10.0f;

	// Use this for initialization
	void Start ()
    {
        musicSource.clip = music;
        musicSource.volume = 0.3f;
        musicSource.loop = true;
        musicSource.Play();
        crowdSource.clip = crowd;
        crowdSource.volume = 0.05f;
        crowdSource.loop = true;
        crowdSource.Play();

        Cursor.visible = false;
        
        System.Array.Copy(Game_Manager.playerData, gameLeaderboard, 4);
        for (int i = 0; i < gameLeaderboard.Length; i++)
        {
            for(int j = 0; j < gameLeaderboard.Length - 1; ++j)
            {
                if(gameLeaderboard[j].playerScore < gameLeaderboard[j + 1].playerScore)
                {
                    Game_Manager.PlayerData temp = gameLeaderboard[j];
                    gameLeaderboard[j] = gameLeaderboard[j + 1];
                    gameLeaderboard[j + 1] = temp;
                }
            }
        }

        for(int x = 0; x < gameLeaderboard.Length; ++x)
        {
            names[x].text = gameLeaderboard[x].playerName;
            kills[x].text = "K[" + gameLeaderboard[x].playerKills.ToString() + "]";
            deaths[x].text = "D[" + gameLeaderboard[x].playerDeaths.ToString() + "]";
            scores[x].text = gameLeaderboard[x].playerScore.ToString();
        }

        carColour();
    }

    // Update is called once per frame
    void Update ()
    {
        GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
        GamePad.SetVibration(PlayerIndex.Two, 0.0f, 0.0f);
        GamePad.SetVibration(PlayerIndex.Three, 0.0f, 0.0f);
        GamePad.SetVibration(PlayerIndex.Four, 0.0f, 0.0f);

        timer += Time.deltaTime;
        if (timer >= wait || XCI.GetButtonDown(XboxButton.Start, XboxController.First) ||
            XCI.GetButtonDown(XboxButton.Start, XboxController.Second) ||
            XCI.GetButtonDown(XboxButton.Start, XboxController.Third) ||
            XCI.GetButtonDown(XboxButton.Start, XboxController.Fourth) ||
            XCI.GetButtonDown(XboxButton.B, XboxController.First) ||
            XCI.GetButtonDown(XboxButton.B, XboxController.Second) ||
            XCI.GetButtonDown(XboxButton.B, XboxController.Third) ||
            XCI.GetButtonDown(XboxButton.B, XboxController.Fourth))
        {
            Game_Manager.playerData[0].playerScore = 0;
            Game_Manager.playerData[1].playerScore = 0;
            Game_Manager.playerData[2].playerScore = 0;
            Game_Manager.playerData[3].playerScore = 0;

            Game_Manager.playerData[0].playerKills = 0;
            Game_Manager.playerData[1].playerKills = 0;
            Game_Manager.playerData[2].playerKills = 0;
            Game_Manager.playerData[3].playerKills = 0;

            Game_Manager.playerData[0].playerDeaths = 0;
            Game_Manager.playerData[1].playerDeaths = 0;
            Game_Manager.playerData[2].playerDeaths = 0;
            Game_Manager.playerData[3].playerDeaths = 0;

            Game_Manager.roundCount = 0;
            Game_Manager.SortLeaderBoard();

            SceneManager.LoadScene(0);
        }
	}
    
    void carColour()
    {
        int first = gameLeaderboard[0].playerID - 1;
        int second = gameLeaderboard[1].playerID - 1;
        int third = gameLeaderboard[2].playerID - 1;
        int fourth = gameLeaderboard[3].playerID - 1;

        carMaterials[0].color = carColours[first];
        carMaterials[1].color = carColours[second];
        carMaterials[2].color = carColours[third];
        carMaterials[3].color = carColours[fourth];

    }
}
