using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform pivot;
    public Vector3 offset = new Vector3(0, 1, 0);


	// Use this for initialization
	void Start ()
    {
        pivot = GameObject.FindGameObjectWithTag("Player").transform.GetChild(5).transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {

		
	}
}
