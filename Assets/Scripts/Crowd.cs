using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

	public AudioSource standsSource;
	public float crowdDistance = 50;
	public Transform[] cars;

	void Update() {
		if (Vector3.Distance(transform.position, cars[0].position) > crowdDistance && Vector3.Distance(transform.position, cars[1].position) > crowdDistance
			&& Vector3.Distance(transform.position, cars[2].position) > crowdDistance && Vector3.Distance(transform.position, cars[3].position) > crowdDistance) {
			fadeOut (standsSource);
		} else {
			fadeIn (standsSource, 0.5f);
		}
	}

	void fadeIn(AudioSource _source, float volumeTo) {
		if (_source.volume < volumeTo) {
			_source.volume += 0.15f * Time.deltaTime;
		} else {
			_source.volume = volumeTo;
		}
	}

	void fadeOut(AudioSource _source) {
		if(_source.volume > 0.1) {
			_source.volume -= 0.15f * Time.deltaTime;
		} else {
			_source.volume = 0;
		}
	}
}
