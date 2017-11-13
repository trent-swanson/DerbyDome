using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour {

	float shakeAmount = 0;

	public void Shake(float amount, float length)
	{
		shakeAmount = amount;
		InvokeRepeating("DoShake", 0, 0.01f);
		Invoke("StopShake", length);
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

	public void StopBoostShake()
	{
		CancelInvoke("DoBoostShake");
		gameObject.transform.localPosition = Vector3.zero;
	}
}
