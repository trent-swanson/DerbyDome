using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip menuMusic;

	void Start()
	{
		if (!audioSource.isPlaying)
		{
			audioSource.clip = menuMusic;
			audioSource.loop = true;
			audioSource.volume = 0.2f;
			audioSource.Play();
		}
	}
}
