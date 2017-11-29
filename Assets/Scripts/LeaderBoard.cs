//================================================================================
//Leaderboard
//
//Purpose: Sorts the end game leaderboard car colors to ensure they drop in the
//correct order
//
//Creator: Trent Swanson
//================================================================================

using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
	public Image[] carImage;
	public Color[] carColour;

	void Start()
	{
        if (Game_Manager.roundCount == 0)
        {
            carImage[0].color = carColour[0];
            carImage[1].color = carColour[1];
            carImage[2].color = carColour[2];
            carImage[3].color = carColour[3];
        }

        else
            UpdateLeaderBoard(Game_Manager.SortLeaderBoardReturn());
	}
	
	public void UpdateLeaderBoard(Game_Manager.PlayerData[] tempPlayerData)
	{
		for (int i = 0; i < tempPlayerData.Length; i++)
		{
			if (tempPlayerData[i].playerID == 1)
				carImage[i].color = carColour[0];
            else if (tempPlayerData[i].playerID == 2)
				carImage[i].color = carColour[1];
            else if (tempPlayerData[i].playerID == 3)
				carImage[i].color = carColour[2];
            else if (tempPlayerData[i].playerID == 4)
				carImage[i].color = carColour[3];
		}
	}
}
