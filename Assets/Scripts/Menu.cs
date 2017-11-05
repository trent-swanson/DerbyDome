using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class Menu : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Debug.Log("Reselecting first input");
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void exit()
    {
        Application.Quit();
    }
}
