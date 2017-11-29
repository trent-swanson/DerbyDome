//================================================================================
//Menu
//
//Purpose: To control the functionality of all main menu buttons
//
//Creator: Trent Swanson
//Edited by: Joel Goodchild
//================================================================================

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

    [Space]

    public AudioSource audioSource;
    public AudioClip selectClip;
    public AudioClip enterClip;

    public GameObject creditsPanel;
    void Start()
    {
        Cursor.visible = false;
    }

    public void PlaySelectSound()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(selectClip, 0.4f);
    }

	void Update ()
    {
        //Ensures that there is always a button selected
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void play()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(enterClip, 0.4f);
        //Enters into the main game scene
        SceneManager.LoadScene(1);
    }

    public void exit()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(enterClip, 0.4f);
        //Quits the game
        Application.Quit();
    }

    public void credits()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(enterClip, 0.4f);

        //Deactivates all buttons, and activates the credits panel and credits exit button to display the credits
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(true);
        events.SetSelectedGameObject(button4);

        creditsPanel.SetActive(true);
    }

    public void creditsExit()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(enterClip, 0.4f);

        //Returns the player back to the main menu buttons and closes the credits
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(false);
        events.SetSelectedGameObject(button1);

        creditsPanel.SetActive(false);
    }
}
