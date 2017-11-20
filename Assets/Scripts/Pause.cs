using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using XboxCtrlrInput;
using XInputDotNetPure;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
	private GameObject pauseCanvas;
    private GameObject gameCanvas;

    public Text[] kills;
    public Text[] deaths;
    public Text[] scores;

	void Start()
	{
		pauseCanvas = GameObject.FindGameObjectWithTag("Pause");
        gameCanvas = GameObject.FindGameObjectWithTag("Canvas");
	}

	void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Debug.Log("Reselecting first input");
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }

		if (XCI.GetButtonUp(XboxButton.Start, XboxController.First) || XCI.GetButtonUp(XboxButton.Start, XboxController.Second))
		{
			if (isPaused){
                gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
				pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
				Time.timeScale = 1f;
                isPaused = false;
			}
			else {
                gameCanvas.transform.GetChild (0).gameObject.SetActive (false);
				pauseCanvas.transform.GetChild (0).gameObject.SetActive (true);
				Time.timeScale = 0f;
				UpdateScoreBoard();
                isPaused = true;
			}
		}

		if (isPaused && XCI.GetButtonUp(XboxButton.B, XboxController.All))
		{
			gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
			pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
			Time.timeScale = 1f;
            isPaused = false;
		}
    }

	void UpdateScoreBoard()
	{
		for (int i = 0; i < Game_Manager.playerData.Length; i++)
		{
			kills[i].text = Game_Manager.playerData[i].playerKills.ToString();
			deaths[i].text = Game_Manager.playerData[i].playerDeaths.ToString();
			scores[i].text = Game_Manager.playerData[i].playerScore.ToString();
		}
	}

    public void Continue()
    {
        gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
		pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
		Time.timeScale = 1f;
        isPaused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
