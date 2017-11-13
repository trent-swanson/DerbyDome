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
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
	public XboxController controller;

	[Space]

	public float enginePower = 100.0f;
    public float breakPower;
	public float maxSpeed = 150.0f;
    private float tempMaxSpeed;
	public float maxSteer = 15.0f;
	public float driftSteer = 35.0f;
    public float driftSidewaysFriction = 1.5f;
    public float driftForwardFriction = 1.5f;
    private float tempSidewaysFriction;
    private float tempForwardFriction;
	public float acclDrag = 0.5f;
	public float declDrag = 2f;
    public float speedMultiplier = 0.0f;
    public float vibrationIntensity = 0.3f;
    public float vibrationThreshold = 3.5f;
    public float carHealth = 1500;
	public float JumpHeight = 15000;

    [Space]
    public float boostSpeed = 200;
    public float boostPower = 100;
    public float boostTimer = 3;
    private float tempBoostTimer;
    public GameObject boostEffect;
    private Slider boostSlider;
    public float boostShake = 0.02f;
    private bool isBoosting = false;
    public CamShake cameraShake;

	[Space]
    public Vector3 localVel;
    public float speed;

	[Space]
	[Space]
	public GameObject[] carParts;
	[Space]
    public Color death = Color.grey;
	[Space]
	public GameObject leftLightTrail;
	public GameObject rightLightTrail;
	public float lightTrailSpeed;

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

    public bool isGrounded = false;
    private Rigidbody playerBody;
	private float CurrentRotation;

    public GameObject skidMarkPrefab;
    public int startingSkidSpeed = 30;
    private GameObject leftRearSkidMark;
    private GameObject rightRearSkidMark;
    private GameObject leftFrontSkidMark;
    private GameObject rightFrontSkidMark;
    private bool groundedFL;
    private bool groundedRL;
    private bool groundedRR;
    private bool groundedFR;

    public GameObject leaderPosition;

    [Space]
	[Space]
	public float speedTreshold = 1;
	public int stepsBelowTreshold = 12;
	public int stepsAboveTreshold = 15;

	[Space]
	[Space]
	[Tooltip("B = Kills player || Y = Revives player")]
	public bool DebugControls = false;

    [Space]
    [Space]
    public GameObject ghostCar;
    public Transform ghostSpawn;
    public Transform cameraDesiredPosition;
    public Transform cameraFocus;
    public Transform cameraPivot;
    public Transform carFront;
    public GameObject camRig;


    //=========================================START===============================================
    void OnEnable()
    {
        Score.OnUpdatePlayerLeader += IsNewLeader;
    }    
    
    void OnDisable()
    {
        Score.OnUpdatePlayerLeader -= IsNewLeader;
    }

    void Start()
    {
		playerBody = transform.GetComponent<Rigidbody>();
		playerBody.centerOfMass = new Vector3(0f, -0.5f, 0.3f);
        playerBody.maxAngularVelocity = 3f;
        //playerBody.maxDepenetrationVelocity = 1f;
        tempMaxSpeed = maxSpeed;
        isGrounded = false;
        
        if(controller == XboxController.First) {
            playerID = 1;
        } else if(controller == XboxController.Second) {
            playerID = 2;
        } else if(controller == XboxController.Third) {
            playerID = 3;
        } else if(controller == XboxController.Fourth) {
            playerID = 4;
        }

        tempForwardFriction = wheelColliders[0].forwardFriction.stiffness;
        tempSidewaysFriction = wheelColliders[0].sidewaysFriction.stiffness;

        CameraSetUp();

        tempBoostTimer = boostTimer;
        boostSlider.minValue = 0;
        boostSlider.maxValue = boostTimer;

		//wheelColliders [0].ConfigureVehicleSubsteps (speedTreshold, stepsBelowTreshold, stepsAboveTreshold);
    }

    //=========================================DRIVE===============================================
    #region Driving 
    private void Turning()
    {
        //Drift Steering
        if (XCI.GetButton(XboxButton.X, controller) && isAlive)
        {
            driftbool = true;
            steer = 0;
            steer = -XCI.GetAxis(XboxAxis.LeftStickX, controller) * driftSteer;
            
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                WheelFrictionCurve frictionCurve = wheelColliders[i].forwardFriction;
                frictionCurve.stiffness = driftForwardFriction;
                wheelColliders[i].forwardFriction = frictionCurve;

                WheelFrictionCurve sideFrictionCurve = wheelColliders[i].sidewaysFriction;
                sideFrictionCurve.stiffness = driftSidewaysFriction;
                wheelColliders[i].sidewaysFriction = sideFrictionCurve;
            }

            wheelColliders[0].steerAngle = 0;
            wheelColliders[1].steerAngle = steer;
            wheelColliders[2].steerAngle = steer;
            wheelColliders[3].steerAngle = 0;
        }
        else if (isAlive)
        {
            driftbool = false;
            steer = 0;
            steer = XCI.GetAxis(XboxAxis.LeftStickX, controller) * maxSteer;

            for (int i = 0; i < wheelColliders.Length; i++)
            {
                WheelFrictionCurve frictionCurve = wheelColliders[i].forwardFriction;
                frictionCurve.stiffness = tempForwardFriction;
                wheelColliders[i].forwardFriction = frictionCurve;

                WheelFrictionCurve sideFrictionCurve = wheelColliders[i].sidewaysFriction;
                sideFrictionCurve.stiffness = tempSidewaysFriction;
                wheelColliders[i].sidewaysFriction = sideFrictionCurve;
            }

            wheelColliders[0].steerAngle = steer;
            wheelColliders[1].steerAngle = 0;
            wheelColliders[2].steerAngle = 0;
            wheelColliders[3].steerAngle = steer;
        }
    }

    private void Accelerate()
    {
        Drag(acclDrag);

        wheelColliders[0].brakeTorque = 0;
        wheelColliders[1].brakeTorque = 0;
        wheelColliders[2].brakeTorque = 0;
        wheelColliders[3].brakeTorque = 0;

        if (isAlive)
        {
            wheelColliders[0].motorTorque = 0;
            wheelColliders[1].motorTorque = power;
            wheelColliders[2].motorTorque = power;
            wheelColliders[3].motorTorque = 0;
        }
    }

    
    private void Reverse()
    {
        Drag(acclDrag);

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

    private void Break(float breakValue)
    {
        Drag(declDrag);

        //Resets the motorTorque of the car to minimise potential bugs, and to stop the player if they are not alive
        //wheelColliders[0].motorTorque = 0;
        //wheelColliders[1].motorTorque = 0;
        //wheelColliders[2].motorTorque = 0;
        //wheelColliders[3].motorTorque = 0;

        wheelColliders[0].brakeTorque = breakValue;
        wheelColliders[1].brakeTorque = breakValue;
        wheelColliders[2].brakeTorque = breakValue;
        wheelColliders[3].brakeTorque = breakValue;
        
    }
    #endregion

	//========================================RAYCASTS=============================================
    #region Flip, Jump, ground check
    private void Jump()
    {
        if (XCI.GetButtonDown(XboxButton.LeftBumper, controller) || XCI.GetButtonDown(XboxButton.RightBumper, controller))
        {
            if(isGrounded == true)
            {
                playerBody.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
            }
        }
    }

	private void GroundCheck()
	{
		RaycastHit hit;
		Ray groundCheck = new Ray(transform.position, -transform.up);
		Debug.DrawRay(transform.position, -transform.up * 0.4f, Color.red);


		if (Physics.Raycast(groundCheck, out hit, 0.4f))
		{
			if (hit.collider.tag == "Ground" || hit.collider.tag == "Player") {
				isGrounded = true;
            } else {
                 isGrounded = false;
            }

	    } else {
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

	IEnumerator WaitForSeconds()
    {
		yield return new WaitForSeconds (3);
		if (isGrounded == false)
		{
			transform.localEulerAngles = new Vector3 (0, CurrentRotation, 0);
		}
	}
    #endregion


    //=========================================UPDATE==============================================
	void Update()
	{
        WheelHit hit;
        groundedFL = wheelColliders[0].GetGroundHit(out hit);
        groundedRL = wheelColliders[1].GetGroundHit(out hit);
        groundedRR = wheelColliders[2].GetGroundHit(out hit);
        groundedFR = wheelColliders[3].GetGroundHit(out hit);

        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(3000);

		GroundCheck();

		//check is upsidedown
		FlipCheck();
        SkidMarks();

        boostSlider.value = tempBoostTimer;
	}

    void FixedUpdate()
    {
        //Debug.Log(playerBody.velocity);

        //car speed in KM/H
        speed = playerBody.velocity.magnitude * 3.6f;
        localVel = playerBody.transform.InverseTransformDirection(playerBody.velocity);

        if (XCI.GetButton(XboxButton.A, controller) && tempBoostTimer > 0 && isAlive)
        {
            isBoosting = true;
            tempBoostTimer -= Time.deltaTime;
            boostEffect.SetActive(true);
            cameraShake.Shake(boostShake);
            tempMaxSpeed = boostSpeed;
            if (speed < tempMaxSpeed)
            {
                playerBody.AddRelativeForce(Vector3.forward * boostPower, ForceMode.Force);
            }
        } else
        {
            isBoosting = false;
            tempMaxSpeed = maxSpeed;
            boostEffect.SetActive(false);
            cameraShake.StopShake();
            if(tempBoostTimer < boostTimer)
            {
                tempBoostTimer += Time.deltaTime / 4;
            }
        }

        if(XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && isAlive || XCI.GetAxis(XboxAxis.LeftTrigger, controller) != 0 && isAlive)
        {
            if (speed > tempMaxSpeed)
            {
                power = 0;
                reverse = 0;
            }
            else
            {
                power = XCI.GetAxis(XboxAxis.RightTrigger, controller) * (enginePower * speedMultiplier) * Time.fixedDeltaTime;
                reverse = XCI.GetAxis(XboxAxis.LeftTrigger, controller) * (enginePower * speedMultiplier) * Time.fixedDeltaTime;
            }
        }

        //Allows for turning and drift
        Turning();

        //Allows the players movement if they are still alive
        if ((XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0) && (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0) && isAlive)
        {
            if (!isBoosting)
            {
                Break(breakPower);
            } else
            {
                return;
            }
        }
        else if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0 && isAlive)
        {
            if (localVel.z < -0.01f)
            {
                Break(breakPower);
            }
            else
            {
                Accelerate();
            }
        }
        else if (XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0 && isAlive && !isBoosting)
        {
            if (localVel.z > 0.01f)
            {
                Break(breakPower);
            }
            else
            {
                Reverse();
            }
        }
        else
        {
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

		//move wheel visuals
		WheelRotation();

        Jump();

        LightTrails();
    }

	//=========================================OTHER=================================================
    #region Other
    void LightTrails()
    {
        if (speed >= lightTrailSpeed && localVel.z > 0.01f)
		{
			leftLightTrail.GetComponent<TrailRenderer> ().enabled = true;
			rightLightTrail.GetComponent<TrailRenderer> ().enabled = true;
		}
		else
		{
			leftLightTrail.GetComponent<TrailRenderer> ().enabled = false;
			rightLightTrail.GetComponent<TrailRenderer> ().enabled = false;
		}
    }

    void SkidMarks()
    {
        bool skidStart = false;
        bool skidEnd = false;
        bool minSkidSpeed = false;

        if (speed < startingSkidSpeed)
            minSkidSpeed = true;
        else
            minSkidSpeed = false;

        bool accelSkid = (minSkidSpeed && (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0));
        bool driftSkid = (XCI.GetButton(XboxButton.X, controller) && XCI.GetAxis(XboxAxis.LeftStickX, controller) != 0);
        bool reverseSkid = (XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0) && (XCI.GetAxis(XboxAxis.LeftStickX, controller) != 0);
        bool brakeSkid = (!minSkidSpeed && XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0) && localVel.z > 0.01f;

        if (accelSkid || driftSkid || reverseSkid || brakeSkid)
            skidStart = true;
        else if (!(groundedFL || groundedRL || groundedRR || groundedFR))
            skidEnd = true;
        else
            skidEnd = true;

        if (skidStart && (leftRearSkidMark == null))
        {
            if (groundedRL)
            {
                leftRearSkidMark = Instantiate(skidMarkPrefab);
                leftRearSkidMark.transform.parent = transform;
                leftRearSkidMark.transform.localPosition = wheelColliders[1].transform.localPosition - Vector3.up * 0.38f;
            }

            if (groundedRR)
            {
                rightRearSkidMark = Instantiate(skidMarkPrefab);
                rightRearSkidMark.transform.parent = transform;
                rightRearSkidMark.transform.localPosition = wheelColliders[2].transform.localPosition - Vector3.up * 0.38f;
            }

            if (driftSkid)
            {
                if (groundedFL)
                {
                    leftFrontSkidMark = Instantiate(skidMarkPrefab);
                    leftFrontSkidMark.transform.parent = transform;
                    leftFrontSkidMark.transform.localPosition = wheelColliders[0].transform.localPosition - Vector3.up * 0.38f;
                }

                if (groundedFR)
                {
                    rightFrontSkidMark = Instantiate(skidMarkPrefab);
                    rightFrontSkidMark.transform.parent = transform;
                    rightFrontSkidMark.transform.localPosition = wheelColliders[3].transform.localPosition - Vector3.up * 0.38f;
                }
            }
        }

        else if (skidEnd && leftRearSkidMark)
        {
            if (leftRearSkidMark != null)
            {
                leftRearSkidMark.transform.parent = null;
                leftRearSkidMark = null;
            }

            if (rightRearSkidMark != null)
            {
                rightRearSkidMark.transform.parent = null;
                rightRearSkidMark = null;
            }

            if (leftFrontSkidMark != null)
            {
                leftFrontSkidMark.transform.parent = null;
                leftFrontSkidMark = null;
            }

            if (rightFrontSkidMark != null)
            {
                rightFrontSkidMark.transform.parent = null;
                rightFrontSkidMark = null;
            }
        }
    }

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

    void IsNewLeader()
    {
        if (playerID == Game_Manager.leaderboard[0].playerID)
        {
            leaderPosition.SetActive(true);
        } else
        {
            leaderPosition.SetActive(false);
        }
    }
    #endregion

    //=========================================DAMAGE===============================================
    public void TakeDamage(float dmgAmount)
    {
        carHealth -= dmgAmount;
        //cameraShake.Shake(0.08f, 0.2f);
        if(carHealth <= 0)
		{
			isAlive = false;
            if (playerID == 1) {
                Game_Manager.playerData[0].playerDeaths++;
            } else if (playerID == 2) {
                Game_Manager.playerData[1].playerDeaths++;
            } else if (playerID == 3) {
                Game_Manager.playerData[2].playerDeaths++;
            } else if (playerID == 4) {
                Game_Manager.playerData[3].playerDeaths++;
            }
			wheelColliders[0].steerAngle = 0;
			wheelColliders[1].steerAngle = 0;
			wheelColliders[2].steerAngle = 0;
			wheelColliders[3].steerAngle = 0;
			wheelColliders[0].steerAngle = 0;
			Break (breakPower);
            for (int i = 0; i < carParts.Length; i++)
			{
				//carParts[i].GetComponent<Renderer> ().material.color = death;
                carParts[i].GetComponent<carPart>().alive = false;
			}
            camRig.SetActive(false);
            GameObject tempGhost = Instantiate(ghostCar, ghostSpawn.position, transform.localRotation);
            CarController tempGhostScript = tempGhost.GetComponent<CarController>();
            tempGhostScript.controller = controller;
            this.enabled = false;
		}
    }

    public void CameraSetUp()
    {
        if (playerID == 1) {
            Debug.Log("Found Cam1");
            GameObject tempPlayerCam = GameObject.FindGameObjectWithTag("Cam1");
            tempPlayerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = tempPlayerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider1").GetComponent<Slider>();
        } else if (playerID == 2) {
            GameObject tempPlayerCam = GameObject.FindGameObjectWithTag("Cam2");
            tempPlayerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = tempPlayerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider2").GetComponent<Slider>();
        } else if (playerID == 3) {
            GameObject tempPlayerCam = GameObject.FindGameObjectWithTag("Cam3");
            tempPlayerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = tempPlayerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider3").GetComponent<Slider>();
        } else if (playerID == 4) {
            GameObject tempPlayerCam = GameObject.FindGameObjectWithTag("Cam4");
            tempPlayerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = tempPlayerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider4").GetComponent<Slider>();
        }
    }
}
