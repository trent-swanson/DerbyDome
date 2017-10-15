using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float myTimer = 180.0f;
    public Text timerText;
    public int roundCounter = 0;

	// Use this for initialization
	void Start ()
    {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        myTimer -= Time.deltaTime;
        timerText.text = myTimer.ToString("f2");
        print(myTimer);
        if (myTimer <= 0.05f)
        {
            SceneManager.LoadScene(0);
            ++roundCounter;
        }
	}
}
