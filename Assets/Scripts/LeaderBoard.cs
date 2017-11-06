using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

	public Image[] carImage;

	public Color[] carColour;

	void Start()
	{
		carImage[0].color = carColour[0];
		carImage[1].color = carColour[1];
		carImage[2].color = carColour[2];
		carImage[3].color = carColour[3];
	}
	
	public void UpdateLeaderBoard(Score.PlayerData[] tempPlayerData)
	{
		for (int i = 0; i < tempPlayerData.Length; i++)
		{
			if (tempPlayerData[i].playerID == 1) {
				carImage[i].color = carColour[0];
			} else if (tempPlayerData[i].playerID == 2) {
				carImage[i].color = carColour[1];
			} else if (tempPlayerData[i].playerID == 3) {
				carImage[i].color = carColour[2];
			} else if (tempPlayerData[i].playerID == 4) {
				carImage[i].color = carColour[3];
			}
		}
	}
}
