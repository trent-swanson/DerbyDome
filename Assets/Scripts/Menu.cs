using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public EventSystem events; 

    public GameObject creditsPanel;
    void Start()
    {
        Cursor.visible = false;
    }

	void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void credits()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(true);
        events.SetSelectedGameObject(button4);

        creditsPanel.SetActive(true);
    }

    public void creditsExit()
    {
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(false);
        events.SetSelectedGameObject(button1);

        creditsPanel.SetActive(false);
    }
}
