using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour {
    Rigidbody2D rb;
    public float speed = -0.8f;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

}
