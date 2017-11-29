//================================================================================
//Pause
//
//Purpose: Controls the in game pause button and pop up pause menu
//
//Creator: Trent Swanson
//Edited by: Joel Goodchild
//================================================================================

using UnityEngine.EventSystems;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject startOption;
    public Text[] kills;
    public Text[] deaths;
    public Text[] scores;

    private bool isPaused = false;
    private GameObject pauseCanvas;
    private GameObject gameCanvas;

    void Start()
	{
		pauseCanvas = GameObject.FindGameObjectWithTag("Pause");
        gameCanvas = GameObject.FindGameObjectWithTag("Canvas");
        pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
	}

	void Update ()
    {
        //Ensures there is always a selected menu option
        if (isPaused && EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(startOption);

        //Checks if any of the players press start to open the pause menu
        if (XCI.GetButtonDown(XboxButton.Start, XboxController.First) || 
            XCI.GetButtonDown(XboxButton.Start, XboxController.Second) || 
            XCI.GetButtonDown(XboxButton.Start, XboxController.Third) || 
            XCI.GetButtonDown(XboxButton.Start, XboxController.Fourth))
		{
            //If a player presses the pause button while they are in the pause menu,
            //they are returned to the game
			if (isPaused)
            {
                gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
                gameCanvas.transform.GetChild (1).gameObject.SetActive (true);
                gameCanvas.transform.GetChild (2).gameObject.SetActive (true);
                EventSystem.current.SetSelectedGameObject(null);
                pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
				Time.timeScale = 1f;
                isPaused = false;
			}

            //If the pause button is pressed while in the game, the pause menu is opened
			else
            {
                gameCanvas.transform.GetChild (0).gameObject.SetActive (false);
                gameCanvas.transform.GetChild (1).gameObject.SetActive (false);
                gameCanvas.transform.GetChild (2).gameObject.SetActive (false);
				pauseCanvas.transform.GetChild (0).gameObject.SetActive (true);
                EventSystem.current.SetSelectedGameObject(startOption);
                Time.timeScale = 0f;
				UpdateScoreBoard();
                isPaused = true;
			}
		}

        //If any of the players press B from the pause menu, they will be sent back to the game
		if (isPaused && XCI.GetButtonUp(XboxButton.B, XboxController.First) || 
            XCI.GetButtonUp(XboxButton.B, XboxController.Second) || 
            XCI.GetButtonUp(XboxButton.B, XboxController.Third) || 
            XCI.GetButtonUp(XboxButton.B, XboxController.Fourth))
		{
			gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
            gameCanvas.transform.GetChild (1).gameObject.SetActive (true);
            gameCanvas.transform.GetChild (2).gameObject.SetActive (true);
			pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
			Time.timeScale = 1f;
            isPaused = false;
		}
    }

    //Updates the scoreboard to display accurate values when the pause menu is opened
	void UpdateScoreBoard()
	{
		for (int i = 0; i < Game_Manager.playerData.Length; i++)
		{
			kills[i].text = Game_Manager.playerData[i].playerKills.ToString();
			deaths[i].text = Game_Manager.playerData[i].playerDeaths.ToString();
			scores[i].text = Game_Manager.playerData[i].playerScore.ToString();
		}
	}

    //Sends the players back to the game if the continue option is selected
    public void Continue()
    {
        EventSystem.current.SetSelectedGameObject(null);
        gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
        gameCanvas.transform.GetChild (1).gameObject.SetActive (true);
        gameCanvas.transform.GetChild (2).gameObject.SetActive (true);
		pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
		Time.timeScale = 1f;
        isPaused = false;
    }

    //Exits the game if the player chooses the quit button
    public void Exit()
    {
        Application.Quit();
    }
}
