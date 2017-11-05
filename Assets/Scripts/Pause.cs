using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using XboxCtrlrInput;
using XInputDotNetPure;

public class Pause : MonoBehaviour {

	private bool isPuased = false;
	private GameObject pauseCanvas;
    private GameObject gameCanvas;

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

		if (XCI.GetButtonUp(XboxButton.Start))
		{
			if (isPuased){
                gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
                gameCanvas.transform.GetChild (1).gameObject.SetActive (true);
				pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
				Time.timeScale = 1f;
				isPuased = false;
			}
			else {
                gameCanvas.transform.GetChild (0).gameObject.SetActive (false);
                gameCanvas.transform.GetChild (1).gameObject.SetActive (false);
				pauseCanvas.transform.GetChild (0).gameObject.SetActive (true);
				Time.timeScale = 0f;
				isPuased = true;
			}
		}
    }

    public void Continue()
    {
        gameCanvas.transform.GetChild (0).gameObject.SetActive (true);
        gameCanvas.transform.GetChild (1).gameObject.SetActive (true);
		pauseCanvas.transform.GetChild (0).gameObject.SetActive (false);
		Time.timeScale = 1f;
		isPuased = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
