//================================================================================
//DownForce
//
//Purpose: Applies downforce to the car to slow it down upon lack of acceleration
//and to keep it on the ground should any of the players end up in the air
//
//Creator: Trent Swanson
//================================================================================

using UnityEngine;

public class DownForce : MonoBehaviour
{

	Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
		rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		rb.AddForce(Vector3.down * 5, ForceMode.Acceleration);
	}
}
