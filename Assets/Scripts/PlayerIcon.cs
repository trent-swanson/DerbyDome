using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour {
	
	public Transform playerTransform;
	public Camera playerCam;
	public int camNumber;
	public int playerNumber;

	[Space]
	public Sprite arrow;
	public Sprite crown;
	public Sprite death;

	Image image;
	bool playerDead = false;

	void OnEnable()
    {
        Score.OnUpdatePlayerLeader += ChangeIcon;
		CarController.OnPlayerDead += DeathIcon;
    }
    
    
    void OnDisable()
    {
        Score.OnUpdatePlayerLeader -= ChangeIcon;
		CarController.OnPlayerDead -= DeathIcon;
    }

	void Start()
	{
		image = transform.GetComponent<Image>();
		
		if (playerNumber == Game_Manager.leaderboard[0].playerID)
		{
			image.sprite = crown;
		} else
		{
			image.sprite = arrow;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = playerCam.WorldToScreenPoint(playerTransform.position);
		if(screenPos.z < 25)
		{
			image.enabled = false;
		} else
		{
			image.enabled = true;
			if (camNumber == 1) {
				transform.position = new Vector3(Mathf.Clamp(screenPos.x, 0, Screen.width / 2), Mathf.Clamp(screenPos.y + 35, Screen.height / 2, Screen.height), 0);
			} else if (camNumber == 2) {
				transform.position = new Vector3(Mathf.Clamp(screenPos.x, Screen.width / 2, Screen.width), Mathf.Clamp(screenPos.y + 35, Screen.height / 2, Screen.height), 0);
			} else if (camNumber == 3) {
				transform.position = new Vector3(Mathf.Clamp(screenPos.x, 0, Screen.width / 2), Mathf.Clamp(screenPos.y + 35, 0, Screen.height / 2), 0);
			} else if (camNumber == 4) {
				transform.position = new Vector3(Mathf.Clamp(screenPos.x, Screen.width / 2, Screen.width), Mathf.Clamp(screenPos.y + 35, 0, Screen.height / 2), 0);
			}
		}
	}

	void DeathIcon()
	{
		image.sprite = death;
		playerDead = true;
	}

	void ChangeIcon()
	{
		if(!playerDead)
		{
			if (playerNumber == Game_Manager.leaderboard[0].playerID)
			{
				image.sprite = crown;
			} else
			{
				image.sprite = arrow;
			}
		}
	}
}
