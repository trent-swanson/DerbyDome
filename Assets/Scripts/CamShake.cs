using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using XInputDotNetPure;

public class CamShake : MonoBehaviour {

	float shakeAmount = 0;
    public XboxController controller;

	public void Shake(float amount, float length)
	{
        vibration(1.0f);
        shakeAmount = amount;
		InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
        Invoke("StopShakeVibration", 0.15f);
    }

	public void Shake(float amount)
	{
        shakeAmount = amount;
        InvokeRepeating("DoShake", 0, 0.01f);
    }

    public void BoostShake(float amount)
	{
		shakeAmount = amount;
		InvokeRepeating("DoBoostShake", 0, 0.01f);
        vibration(0.3f);
    }

    void DoShake()
	{
		if(shakeAmount > 0)
		{
			if(gameObject.transform.position.x > 0.15f || gameObject.transform.position.x < -0.15f ||
			gameObject.transform.position.y < -0.15f || gameObject.transform.position.y < -0.15f)
			{
				gameObject.transform.localPosition = Vector3.zero;
			}

			Vector3 camPos = gameObject.transform.position;

			float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
			camPos.x += offsetX;
			camPos.y += offsetY;

			gameObject.transform.position = camPos;
		}
	}

	void DoBoostShake()
	{
		if(shakeAmount > 0)
		{
			if(gameObject.transform.position.x > 0.15f || gameObject.transform.position.x < -0.15f ||
			gameObject.transform.position.y < -0.15f || gameObject.transform.position.y < -0.15f)
			{
				gameObject.transform.localPosition = Vector3.zero;
			}

			Vector3 camPos = gameObject.transform.position;

			float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
			camPos.x += offsetX;
			camPos.y += offsetY;

			gameObject.transform.position = camPos;
		}
	}

	public void StopShake()
	{
        CancelInvoke("DoShake");
		gameObject.transform.localPosition = Vector3.zero;
	}

    public void StopShakeVibration()
    {
        stopVibration();
    }

    public void StopBoostShake()
	{
		CancelInvoke("DoBoostShake");
		gameObject.transform.localPosition = Vector3.zero;
        stopVibration();
	}

    void vibration(float intensity)
    {
        int playerNum;

        if (controller == XboxController.First)
            playerNum = 1;
        else if (controller == XboxController.Second)
            playerNum = 2;
        else if (controller == XboxController.Third)
            playerNum = 3;
        else if (controller == XboxController.Fourth)
            playerNum = 4;
        else
            playerNum = 0;

        //Sets the vibration if player one is the active player in the update function
        if (playerNum == 1)
            GamePad.SetVibration(PlayerIndex.One, intensity, intensity);
        //Sets the vibration if player two is the active player in the update function
        else if (playerNum == 2)
            GamePad.SetVibration(PlayerIndex.Two, intensity, intensity);
        //Sets the vibration if player three is the active player in the update function
        else if (playerNum == 3)
            GamePad.SetVibration(PlayerIndex.Three, intensity, intensity);
        //Sets the vibration if player four is the active player in the update function
        else if (playerNum == 4)
            GamePad.SetVibration(PlayerIndex.Four, intensity, intensity);
        else
            return;
    }

    void stopVibration()
    {
        int playerNum;

        if (controller == XboxController.First)
            playerNum = 1;
        else if (controller == XboxController.Second)
            playerNum = 2;
        else if (controller == XboxController.Third)
            playerNum = 3;
        else if (controller == XboxController.Fourth)
            playerNum = 4;
        else
            playerNum = 0;

        //Sets the vibration if player one is the active player in the update function
        if (playerNum == 1)
            GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
        //Sets the vibration if player two is the active player in the update function
        else if (playerNum == 2)
            GamePad.SetVibration(PlayerIndex.Two, 0.0f, 0.0f);
        //Sets the vibration if player three is the active player in the update function
        else if (playerNum == 3)
            GamePad.SetVibration(PlayerIndex.Three, 0.0f, 0.0f);
        //Sets the vibration if player four is the active player in the update function
        else if (playerNum == 4)
            GamePad.SetVibration(PlayerIndex.Four, 0.0f, 0.0f);
        else
            return;
    }
}
