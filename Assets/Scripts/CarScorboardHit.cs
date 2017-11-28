using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScorboardHit : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip hitSound;
	bool hasPlayed = false;

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Ground" || other.collider.tag == "Player"){
			if (!hasPlayed)
			{
				audioSource.PlayOneShot(hitSound, 0.65f);
				hasPlayed = true;
			}
		}
	}
}
