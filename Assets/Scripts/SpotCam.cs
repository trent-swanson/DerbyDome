﻿//================================================================================
//SpotCam
//
//Purpose: Allows the TV's to follow the leading players car
//
//Creator: Trent Swanson
//Edited by: Joel Goodchild
//================================================================================

using UnityEngine;

public class SpotCam : MonoBehaviour {

	Transform leader;

	void OnEnable()
    {
        Score.OnLateUpdatePlayerLeader += NewLeaderCam;
    }
    
    
    void OnDisable()
    {
        Score.OnLateUpdatePlayerLeader -= NewLeaderCam;
    }

	void NewLeaderCam()
	{
        if (!Game_Manager.isDraw)
            leader = GameObject.FindGameObjectWithTag("Leader").transform;
	}

	void Update()
	{
        if (leader)
        {
            transform.position = leader.GetChild(0).transform.position;
            leader.Rotate(Vector3.up * 30 * Time.deltaTime);
            transform.LookAt(leader);
        }
	}
}
