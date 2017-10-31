//================================================================================
//CarController
//
//Purpose: To control the players movement, store the players health
//and determine when the player is dead
//
//Creator: Joel Goodchild
//Edited by: Ryan Ward
//Edited by: Trent Swanson
//================================================================================

using UnityEngine;
using XboxCtrlrInput;
using XInputDotNetPure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
	public XboxController controller;

	[Space]

	public Transform centerOfMass;
	public float enginePower = 100.0f;
	public float maxFWVelocity = 10.0f;
    public float maxBWVelocity = 5.0f;
	public float maxSteer = 15.0f;
	public float driftSteer = 35.0f;
	public float acclDrag = 0.5f;
	public float declDrag = 2f;
    public float speedMultiplier = 0.0f;
    public float vibrationIntensity = 0.3f;
    public float vibrationThreshold = 3.5f;
    public float carHealth = 1500;
	public float JumpHeight = 15000;

	[Space]
    public Vector3 localVel;

	[Space]
	[Space]
	public GameObject[] carParts;
	[Space]
    public Color death = Color.grey;

	[Space]
    public WheelCollider[] wheelColliders;
	public Transform[] wheels;

    [HideInInspector]
    public float power = 0.0f;
    [HideInInspector]
    public float reverse = 0.0f;
    [HideInInspector]
    public float steer = 0.0f;
    [HideInInspector]
    public bool driftbool = false;
    [HideInInspector]
    public int playerID;
    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public Color debugAlive = Color.magenta;

    private bool isGrounded = false;
    private Rigidbody playerBody;
	private float CurrentRotation;

	[Space]
	[Space]
	public float speedTreshold = 1;
	public int stepsBelowTreshold = 12;
	public int stepsAboveTreshold = 15;

	[Space]
	[Space]
	[Tooltip("B = Kills player || Y = Revives player")]
	public bool DebugControls = false;
    //==============================================================================================
    // Use this for initialization
    void Start()
    {
		playerBody = transform.GetComponent<Rigidbody>();
		playerBody.centerOfMass = new Vector3(0f, -0.5f, 0.3f);
		//wheelColliders [0].ConfigureVehicleSubsteps (speedTreshold, stepsBelowTreshold, stepsAboveTreshold);
    }

    //==============================================================================================
    private void Break(float breakValue)
    {
        Drag(declDrag);

        //Resets the motorTorque of the car to minimise potential bugs, and to stop the player if they are not alive
        wheelColliders[0].motorTorque = 0;
        wheelColliders[1].motorTorque = 0;
        wheelColliders[2].motorTorque = 0;
        wheelColliders[3].motorTorque = 0;

        //If the player is alive and currently breaking, the appropriate break torque is applied
        if (isAlive)
        {
            wheelColliders[0].brakeTorque = breakValue;
            wheelColliders[1].brakeTorque = breakValue;
            wheelColliders[2].brakeTorque = breakValue;
            wheelColliders[3].brakeTorque = breakValue;
        }
        
    }

    //==============================================================================================
    private void Reverse()
    {
        Drag(acclDrag);

		if (localVel.z > 0.01f)
		{
			Break(reverse);
			return;
		}

		wheelColliders[0].brakeTorque = 0;
		wheelColliders[1].brakeTorque = 0;
		wheelColliders[2].brakeTorque = 0;
		wheelColliders[3].brakeTorque = 0;

        if (isAlive)
        {
            wheelColliders[0].motorTorque = 0;
            wheelColliders[1].motorTorque = -reverse;
            wheelColliders[2].motorTorque = -reverse;
            wheelColliders[3].motorTorque = 0;
        }
    }

    //==============================================================================================
    private void Drift()
    {
        //Drift Steering
        if (XCI.GetButton(XboxButton.X, controller) && isAlive)
        {
            driftbool = true;
            steer = 0;
            steer = -XCI.GetAxis(XboxAxis.LeftStickX, controller) * driftSteer;
            wheelColliders[0].steerAngle = 0;
            wheelColliders[1].steerAngle = steer;
            wheelColliders[2].steerAngle = steer;
            wheelColliders[3].steerAngle = 0;
        }

        else if (isAlive)
        {
            driftbool = false;
            steer = 0;
            steer = XCI.GetAxis(XboxAxis.LeftStickX, controller) * maxSteer * ((localVel.z / 120) + 0.8f);
            wheelColliders[0].steerAngle = steer;
            wheelColliders[1].steerAngle = 0;
            wheelColliders[2].steerAngle = 0;
            wheelColliders[3].steerAngle = steer;
        }
    }

    //==============================================================================================
    private void Accelerate()
    {
        Drag(acclDrag);

		if (localVel.z < -0.01f && isAlive)
		{
			Break(power);
			return;
		}

        wheelColliders[0].brakeTorque = 0;
        wheelColliders[1].brakeTorque = 0;
        wheelColliders[2].brakeTorque = 0;
        wheelColliders[3].brakeTorque = 0;

        wheelColliders[0].motorTorque = 0;
        wheelColliders[1].motorTorque = 0;
        wheelColliders[2].motorTorque = 0;
        wheelColliders[3].motorTorque = 0;

        if (isAlive)
        {
            wheelColliders[0].motorTorque = 0;
            wheelColliders[1].motorTorque = power;
            wheelColliders[2].motorTorque = power;
            wheelColliders[3].motorTorque = 0;
        }
    }

	//==============================================================================================

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerBody.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
            Debug.Log("Is Firing");
        }
    }

    //==============================================================================================

	private void GroundCheck()
	{
        isGrounded = false;
		RaycastHit hit;
		Ray groundCheck = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.red);

		if (Physics.Raycast(groundCheck, out hit, 0.3f))
		{
			if (hit.collider.tag == "Ground")
				isGrounded = true;
			else
				isGrounded = false;
		}
	}

	private void FlipCheck()
	{
		CurrentRotation = transform.eulerAngles.y;
		RaycastHit hit;
		Ray groundCheck = new Ray(transform.position, transform.up);
		Debug.DrawRay(transform.position, transform.up * 1.4f, Color.yellow);

		if (Physics.Raycast(groundCheck, out hit, 1.4f))
		{
			if (hit.collider.tag == "Ground")
			{
				StartCoroutine ("WaitForSeconds");
			}
		}
	}
	IEnumerator WaitForSeconds() {
		yield return new WaitForSeconds (3);
		if (isGrounded == false)
		{
			transform.localEulerAngles = new Vector3 (0, CurrentRotation, 0);
		}
	}

    //==============================================================================================
    // Update is called once per frame
    void FixedUpdate()
    {
        //Checks the players health and sets them to not alive if it is below zero
        if(carHealth <= 0)
		{
			isAlive = false;
			wheelColliders[0].steerAngle = 0;
			wheelColliders[1].steerAngle = 0;
			wheelColliders[2].steerAngle = 0;
			wheelColliders[3].steerAngle = 0;
			wheelColliders[0].steerAngle = 0;
			Break (power);
		}

        if (XCI.GetButton(XboxButton.B, controller) && DebugControls)
            isAlive = false;

        if (XCI.GetButton(XboxButton.Y, controller) && DebugControls)
        {
            isAlive = true;
            transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = debugAlive;
        }

        if (XCI.GetButton(XboxButton.Start, controller))
        {
            SceneManager.LoadScene(0);
        }

        //Changes color to grey if the players health is below zero
        if (!isAlive)
		{
			//transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = death;
			for (int i = 0; i < carParts.Length; i++)
			{
				carParts [i].GetComponent<Renderer> ().material.color = death;
			}
		}

        //Calculates the amount of power that will be applied to the wheels, depending on the movement of the player 
        //and if they are currently alive
        else
        {
            power = XCI.GetAxis(XboxAxis.RightTrigger, controller) * (enginePower * speedMultiplier) * Time.fixedDeltaTime;
            reverse = XCI.GetAxis(XboxAxis.LeftTrigger, controller) * (enginePower * speedMultiplier) * Time.fixedDeltaTime;
            localVel = playerBody.transform.InverseTransformDirection(playerBody.velocity);
        }

        //Sets up controller numbers to ensure that vibration is applied to the correct controller
        if (controller == XboxController.First)
            playerID = 1;
        else if (controller == XboxController.Second)
            playerID = 2;
        else if (controller == XboxController.Third)
            playerID = 3;
        else if (controller == XboxController.Fourth)
            playerID = 4;
        else
            playerID = 0;

		//ground check
		GroundCheck();

		//check is upsidedown
		FlipCheck();
            

            if (localVel.z >= maxFWVelocity)
            {
                float temp = localVel.z - maxFWVelocity;
                localVel.z -= temp;
            }

            if (localVel.z <= -maxBWVelocity)
            {
                float temp = localVel.z + maxBWVelocity;
                localVel.z -= temp;
            }

            //Allows for drift mode
            Drift();

            //Allows the players movement if they are still alive
            if ((XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0) && (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0) && isAlive)
            {
                Break(power);
                //Sets vibration
                Vibration(playerID, vibrationIntensity, vibrationIntensity);
            }

            else if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0 && isAlive)
            {
                Accelerate();
                //Sets vibration
                Vibration(playerID, vibrationIntensity, vibrationIntensity);
            }

            else if (XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0 && isAlive)
            {
                if (localVel.z > 8)
                {
                    Break(power);
                    //Sets vibration
                    Vibration(playerID, vibrationIntensity, vibrationIntensity);
                }

                else
                {
                    Reverse();
                    //Sets vibration
                    Vibration(playerID, vibrationIntensity, vibrationIntensity);
                }
            }

            else
            {
                //Removes vibration to ensure players that are not moving are not vibrating if they are below the threshold
                if (localVel.z > vibrationThreshold || localVel.z < -vibrationThreshold)
                    Vibration(playerID, vibrationIntensity, vibrationIntensity);

                else if (localVel.z <= vibrationThreshold || localVel.z >= -vibrationThreshold)
                    Vibration(playerID, 0.0f, 0.0f);

                if (isGrounded)
                {
                    if (localVel.z > 7f || localVel.z < -7f)
                    {
                        playerBody.drag = declDrag / 2;
                    }

                    else if (localVel.z > 4f || localVel.z < -4f)
                    {
                        playerBody.drag = declDrag;
                    }

                    else
                    {
                        playerBody.drag = declDrag * 2;
                    }
                }

                else
                {
                    playerBody.drag = 0;
                }

                wheelColliders[0].motorTorque = 0;
                wheelColliders[1].motorTorque = 0;
                wheelColliders[2].motorTorque = 0;
                wheelColliders[3].motorTorque = 0;
            }

		//turn wheels
		WheelRotation();

        //Jump
        if (isGrounded)
        {
            Jump();
        }
    }

	//==============================================================================================
	void WheelRotation()
	{
		if (!driftbool)
		{
			wheels [0].localEulerAngles = new Vector3 (wheels [0].localEulerAngles.x, wheelColliders [0].steerAngle - wheels [0].localEulerAngles.z, wheels [0].localEulerAngles.z);
			wheels [1].localEulerAngles = new Vector3 (wheels [1].localEulerAngles.x, (wheelColliders [3].steerAngle - wheels [1].localEulerAngles.z) + 180, wheels [1].localEulerAngles.z);
		} else
		{
			wheels [0].localEulerAngles = new Vector3 (wheels [0].localEulerAngles.x, -(wheelColliders [1].steerAngle - wheels [0].localEulerAngles.z), wheels [0].localEulerAngles.z);
			wheels [1].localEulerAngles = new Vector3 (wheels [1].localEulerAngles.x, -((wheelColliders [2].steerAngle - wheels [1].localEulerAngles.z) + 180), wheels [1].localEulerAngles.z);
		}
		wheels[0].Rotate(wheelColliders[0].rpm / 60 * 360 * Time.deltaTime, 0, 0);
		wheels[1].Rotate(wheelColliders[3].rpm / 60 * 360 * Time.deltaTime, 0, 0);
		wheels[2].Rotate(wheelColliders[1].rpm / 60 * 360 * Time.deltaTime, 0, 0);
		wheels[3].Rotate(wheelColliders[2].rpm / 60 * 360 * Time.deltaTime, 0, 0);
	}

    //==============================================================================================
    void Vibration(int playerNum, float left, float right)
    {
        //Sets the vibration if player one is the active player in the update function
        if (playerNum == 1)
            GamePad.SetVibration(PlayerIndex.One, left, right);
        //Sets the vibration if player two is the active player in the update function
        else if (playerNum == 2)
            GamePad.SetVibration(PlayerIndex.Two, left, right);
        //Sets the vibration if player three is the active player in the update function
        else if (playerNum == 3)
            GamePad.SetVibration(PlayerIndex.Three, left, right);
        //Sets the vibration if player four is the active player in the update function
        else if (playerNum == 4)
            GamePad.SetVibration(PlayerIndex.Four, left, right);
        else
            return;
    }

    //==============================================================================================
    void Drag(float typeOfDrag)
    {
        //Checks if the player is on the ground and applies drag to them
        if (isGrounded)
        {
            playerBody.drag = typeOfDrag;
        }

        //If the player is not on the ground, no drag is applied to them so they dont slow down travelling through the air
        else
        {
            playerBody.drag = 0;
        }
    }
}
