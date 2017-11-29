//================================================================================
//CarScoreboardHit
//
//Purpose: Controls the audio of the cars colliding in the end game scoreboard scene
//
//Creator: Trent Swanson
//================================================================================

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
