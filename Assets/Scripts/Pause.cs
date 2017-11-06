using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using XboxCtrlrInput;
using XInputDotNetPure;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	private bool isPuased = false;
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

		if (XCI.GetButtonUp(XboxButton.Start, XboxController.All))
		{
			if (isPuased){
                gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
				pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
				Time.timeScale = 1f;
				isPuased = false;
			}
			else {
                gameCanvas.transform.GetChild (0).gameObject.SetActive (false);
				pauseCanvas.transform.GetChild (0).gameObject.SetActive (true);
				Time.timeScale = 0f;
				UpdateScoreBoard();
				isPuased = true;
			}
		}
    }

	void UpdateScoreBoard()
	{
		for (int i = 0; i < Score.playerData.Length; i++)
		{
			kills[i].text = Score.playerData[i].playerKills.ToString();
			deaths[i].text = Score.playerData[i].playerDeaths.ToString();
			scores[i].text = Score.playerData[i].playerScore.ToString();
		}
	}

    public void Continue()
    {
        gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
		pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
		Time.timeScale = 1f;
		isPuased = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
