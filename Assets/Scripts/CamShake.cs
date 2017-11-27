using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using XInputDotNetPure;

public class CamShake : MonoBehaviour
{

	float shakeAmount = 0;
    public XboxController controller;

	public void Shake(float amount, float length)
	{
        PlayerIndex playerNum = checkPlayer(controller);
        vibration(playerNum, 1.0f);
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
        PlayerIndex playerNum = checkPlayer(controller);
        shakeAmount = amount;
		InvokeRepeating("DoBoostShake", 0, 0.01f);
        vibration(playerNum, 0.3f);
    }

    void DoShake()
	{
		if(shakeAmount > 0)
		{
			if(gameObject.transform.position.x > 0.15f || gameObject.transform.position.x < -0.15f ||
			gameObject.transform.position.y < -0.15f || gameObject.transform.position.y < -0.15f)			
				gameObject.transform.localPosition = Vector3.zero;			

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
				gameObject.transform.localPosition = Vector3.zero;			

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
        PlayerIndex playerNum = checkPlayer(controller);
        stopVibration(playerNum);
    }

    public void StopBoostShake()
	{
        PlayerIndex playerNum = checkPlayer(controller);

        CancelInvoke("DoBoostShake");
		gameObject.transform.localPosition = Vector3.zero;
        stopVibration(playerNum);
	}

    void vibration(PlayerIndex playerNum ,float intensity)
    {       
        GamePad.SetVibration(playerNum, intensity, intensity);
    }

    void stopVibration(PlayerIndex playerNum)
    {
        GamePad.SetVibration(playerNum, 0.0f, 0.0f);
    }

    PlayerIndex checkPlayer(XboxController control)
    {
        if (controller == XboxController.First)
            return PlayerIndex.One;
        else if (controller == XboxController.Second)
            return PlayerIndex.Two;
        else if (controller == XboxController.Third)
            return PlayerIndex.Three;
        else if (controller == XboxController.Fourth)
            return PlayerIndex.Four;
        else
            return 0;
    }
}
