using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownForce : MonoBehaviour {

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce(Vector3.down * 5, ForceMode.Acceleration);
	}
}
