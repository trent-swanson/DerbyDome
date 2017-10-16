using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using XInputDotNetPure;

public class CarController : MonoBehaviour
{
	public XboxController controller;

	[Space]

	public float enginePower = 100.0f;
	public float MaxFWVelocity = 10.0f;
    public float MaxBWVelocity = 5.0f;
	public float maxSteer = 15.0f;
	public float driftSteer = 35.0f;
	public float acclDrag = 0.5f;
	public float declDrag = 2f;
    public float speedMultiplier = 0.0f;
    public float vibrationIntensity = 0.3f;
    public float vibrationThreshold = 3.5f;
    public float carHealth = 1500;
    public int playerID;


    //[HideInInspector] commented out to debug
    public float power = 0.0f;
	[HideInInspector]
	public float reverse = 0.0f;
	[HideInInspector]
	public float steer = 0.0f;
	[HideInInspector]
	public bool driftbool = false;

    private Rigidbody playerBody;
	public Vector3 localVel;
	private bool isGrounded = false;

	[Space]
	[Space]

	public WheelCollider[] wheelColliders;

    // Use this for initialization
    void Start()
    {
		playerBody = transform.GetComponent<Rigidbody>();
		playerBody.centerOfMass = new Vector3(0.0f, -0.5f, 0.3f);
    }

	private void Break(float breakValue)
    {
		if (isGrounded)
		{
			playerBody.drag = declDrag;
			playerBody.angularDrag = declDrag;
		}

		else
		{
			playerBody.drag = 0;
			playerBody.angularDrag = 0;	
		}

		wheelColliders[0].motorTorque = 0;
		wheelColliders[1].motorTorque = 0;
		wheelColliders[2].motorTorque = 0;
		wheelColliders[3].motorTorque = 0;
		wheelColliders[0].brakeTorque = breakValue;
		wheelColliders[1].brakeTorque = breakValue;
		wheelColliders[2].brakeTorque = breakValue;
		wheelColliders[3].brakeTorque = breakValue;
    }

    private void Reverse()
    {
		if (isGrounded)
		{
            playerBody.drag = acclDrag;
			playerBody.angularDrag = acclDrag;
		}
		else
		{
			playerBody.drag = 0;
			playerBody.angularDrag = 0;	
		}

		if (localVel.z > 0.01f)
		{
			Break(reverse);
			return;
		}

		wheelColliders[0].brakeTorque = 0;
		wheelColliders[1].brakeTorque = 0;
		wheelColliders[2].brakeTorque = 0;
		wheelColliders[3].brakeTorque = 0;

        wheelColliders[0].motorTorque = 0;
        wheelColliders[1].motorTorque = -reverse;
        wheelColliders[2].motorTorque = -reverse;
        wheelColliders[3].motorTorque = 0;
    }

    private void Drift()
    {
        //Drift Steering
        if (XCI.GetButton(XboxButton.X, controller))
        {
            driftbool = true;
            steer = 0;
            steer = -XCI.GetAxis(XboxAxis.LeftStickX, controller) * driftSteer;
            wheelColliders[1].steerAngle = steer;
            wheelColliders[2].steerAngle = steer;
            wheelColliders[0].steerAngle = 0;
            wheelColliders[3].steerAngle = 0;
        }

        else
        {
            driftbool = false;
            steer = 0;
            steer = XCI.GetAxis(XboxAxis.LeftStickX, controller) * maxSteer * ((localVel.z / 120) + 0.8f);
            wheelColliders[1].steerAngle = 0;
            wheelColliders[2].steerAngle = 0;
            wheelColliders[0].steerAngle = steer;
            wheelColliders[3].steerAngle = steer;
        }
    }

    private void Accelerate()
    {
		if (isGrounded)
		{
			playerBody.drag = acclDrag;
			playerBody.angularDrag = acclDrag;
		}

		else
		{
			playerBody.drag = 0;
			playerBody.angularDrag = 0;	
		}

		if (localVel.z < -0.01f)
		{
			Break(power);
			return;
		}

        wheelColliders[0].brakeTorque = 0;
        wheelColliders[1].brakeTorque = 0;
        wheelColliders[2].brakeTorque = 0;
        wheelColliders[3].brakeTorque = 0;

        wheelColliders[1].motorTorque = power;
        wheelColliders[2].motorTorque = power;
        //front
        wheelColliders[0].motorTorque = 0;
        wheelColliders[3].motorTorque = 0;
    }


    // Update is called once per frame
    void Update()
    {
        power = XCI.GetAxis (XboxAxis.RightTrigger, controller) * (enginePower * speedMultiplier) * Time.deltaTime;
		reverse = XCI.GetAxis (XboxAxis.LeftTrigger, controller) * (enginePower * speedMultiplier) * Time.deltaTime;
		localVel = playerBody.transform.InverseTransformDirection(playerBody.velocity);

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

        if (localVel.z >= MaxFWVelocity)
        {
            float temp = localVel.z - MaxFWVelocity;
            localVel.z -= temp;
        }

        if (localVel.z <= -MaxBWVelocity)
        {
            float temp = localVel.z + MaxBWVelocity;
            localVel.z -= temp;
        }

        //Allows for drift mode
        Drift();

        if ((XCI.GetAxis (XboxAxis.LeftTrigger, controller) > 0) && (XCI.GetAxis (XboxAxis.RightTrigger, controller) > 0))
        {
			Break(power);
            //Sets vibration
            Vibration(playerID, vibrationIntensity, vibrationIntensity);
        }

		else if (XCI.GetAxis (XboxAxis.RightTrigger, controller) > 0)
        {
			Accelerate();
            //Sets vibration
            Vibration(playerID, vibrationIntensity, vibrationIntensity);
        }

		else if (XCI.GetAxis (XboxAxis.LeftTrigger, controller) > 0)
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
				if (localVel.z > 7.5f || localVel.z < -7.5f)
				{
					playerBody.drag = declDrag / 2;
					playerBody.angularDrag = declDrag / 2;
				}

				else if (localVel.z > 3.6f || localVel.z < -3.6f)
				{
                    playerBody.drag = declDrag;
                    playerBody.angularDrag = declDrag;
                }

				else
				{
					playerBody.drag = declDrag * 2;
					playerBody.angularDrag = declDrag * 2;
				}
			}

			else
			{
				playerBody.drag = 0;
				playerBody.angularDrag = 0;	
			}

            wheelColliders[0].motorTorque = 0;
            wheelColliders[1].motorTorque = 0;
            wheelColliders[2].motorTorque = 0;
            wheelColliders[3].motorTorque = 0;
        }

        
    }
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
}
