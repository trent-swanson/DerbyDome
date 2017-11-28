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
using UnityEngine.PostProcessing;

public class CarController : MonoBehaviour
{
    public AudioClip idleSound;
    public AudioClip accSound;
    public AudioClip accSoundHigh;
    public AudioClip decSound;
    public AudioClip[] impactSounds;
    public AudioClip explosionSound;
    public AudioClip[] skidSounds;
    public AudioClip boostSound;
    public AudioSource mainSource1;
    public AudioSource mainSource2;
    public AudioSource mainSource3;
    AudioSource audioSource1;
    AudioSource audioSource2;
    float audio1Volume = 1.0f;
    float audio2Volume = 0.0f;
    bool track2Playing = false;

    [Space]
    [Space]
    [Space]
	public XboxController controller;

	[Space]
    public GameObject spawner;
    public bool isGhost = false;

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
    public float carHealth = 1500;
    private float savedCarHealth;
    private bool isDamaged = false;
    public Animator carAnimator;
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
    public PostProcessingProfile m_Profile;
    private GameObject playerCam;

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
    [HideInInspector]
    public static bool canControl;

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

    public GameObject impactEffect;
    public float hitImpactTimer = 3f;
    private float impactTimer;
    public float minAttackSpeed = 30f;
    public float impactShake = 0.02f;
    public float impactShakeTime = 0.02f;
    public GameObject explosion;
    public SphereCollider explosionSphere;
    public GameObject stage1Smoke;
    public GameObject stage2Smoke;
    public GameObject stage3Smoke;
    public GameObject tailSmoke;
    private bool deathTimerStart;
    private float deathTimer;

    [Space]
    [Space]

    //[FMODUnity.EventRef]
	public string selectsound = "event:/Audio";
	//FMOD.Studio.EventInstance soundevent;

    public delegate void PlayerDeadAction(int num);
    public static event PlayerDeadAction OnPlayerDead;


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
        audioSource1 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource2 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        if(!audioSource1.isPlaying) {
            audioSource1.clip = idleSound;
            audioSource1.loop = true;
            audioSource1.volume = 0.5f;
            audioSource1.Play();
        }

        savedCarHealth = carHealth;

        if (!isGhost)
            Instantiate(spawner, transform.position, Quaternion.identity);

        impactTimer = hitImpactTimer;
		playerBody = transform.GetComponent<Rigidbody>();
		playerBody.centerOfMass = new Vector3(0f, -0.5f, 0.3f);
        playerBody.maxAngularVelocity = 3f;
        //playerBody.maxDepenetrationVelocity = 1f;
        tempMaxSpeed = maxSpeed;
        isGrounded = false;
        
        if(controller == XboxController.First)
            playerID = 1;
        else if(controller == XboxController.Second)
            playerID = 2;
        else if(controller == XboxController.Third)
            playerID = 3;
        else if(controller == XboxController.Fourth)
            playerID = 4;

        tempForwardFriction = wheelColliders[0].forwardFriction.stiffness;
        tempSidewaysFriction = wheelColliders[0].sidewaysFriction.stiffness;

        CameraSetUp();

        PostProcessingBehaviour behaviour = playerCam.GetComponent<PostProcessingBehaviour>();
        if (behaviour.profile == null)
        {
            enabled = false;
            return;
        }

        m_Profile = Instantiate(behaviour.profile);
        behaviour.profile = m_Profile;

        ColorGradingModel.Settings colorSettings = m_Profile.colorGrading.settings;
        float saveSatValue = colorSettings.basic.saturation;
        if(isGhost)
        {
            colorSettings.basic.saturation = 0.3f;
            m_Profile.colorGrading.settings = colorSettings;
        } else {
            colorSettings.basic.saturation = saveSatValue;
            m_Profile.colorGrading.settings = colorSettings;
        }

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
        //soundevent.start();

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
        wheelColliders[0].motorTorque = 0;
        wheelColliders[1].motorTorque = 0;
        wheelColliders[2].motorTorque = 0;
        wheelColliders[3].motorTorque = 0;

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
                playerBody.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }
    }

	private void GroundCheck()
	{
		RaycastHit hit;
		Ray groundCheck = new Ray(transform.position, -transform.up);
		Debug.DrawRay(transform.position, -transform.up * 0.4f, Color.red);


		if (Physics.Raycast(groundCheck, out hit, 0.4f))
		{
			if (hit.collider.tag == "Ground" || hit.collider.tag == "Player")
				isGrounded = true;
            else
                 isGrounded = false;
	    }
        else
            isGrounded = false;
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
				StartCoroutine ("WaitForSeconds");
		}
	}

	IEnumerator WaitForSeconds()
    {
		yield return new WaitForSeconds (3);
		if (isGrounded == false)
			transform.localEulerAngles = new Vector3 (0, CurrentRotation, 0);
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

        if (DebugControls)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TakeDamage(3000);
        }

		GroundCheck();
		FlipCheck();
        SkidMarks();

        boostSlider.value = tempBoostTimer;

        if (!isGhost)
        {
            if (impactTimer > 0)
                impactTimer -= Time.deltaTime;
            if (carAnimator)
            {
                if (speed >= 70 && isDamaged)
                    carAnimator.SetBool("Moving", true);
                else
                    carAnimator.SetBool("Moving", false);
            }

            if (carHealth <= savedCarHealth / 10)
            {
                stage1Smoke.SetActive(false);
                stage2Smoke.SetActive(false);
                stage3Smoke.SetActive(true);
                tailSmoke.SetActive(true);
            }

            else if (carHealth <= savedCarHealth / 4)
            {
                stage1Smoke.SetActive(false);
                stage2Smoke.SetActive(true);
                stage3Smoke.SetActive(false);
                tailSmoke.SetActive(true);
            }

            else if (carHealth <= savedCarHealth / 2)
            {
                stage1Smoke.SetActive(true);
                stage2Smoke.SetActive(false);
                stage3Smoke.SetActive(false);
                tailSmoke.SetActive(false);
            }

            else
            {
                if (!ghostCar)
                {
                    stage1Smoke.SetActive(false);
                    stage2Smoke.SetActive(false);
                    stage3Smoke.SetActive(false);
                    tailSmoke.SetActive(false);
                }
            }
        }
        //EngineSound();
    }

    void FixedUpdate()
    {
        if (canControl)
        {
            //car speed in KM/H
            speed = playerBody.velocity.magnitude * 3.6f;
            localVel = playerBody.transform.InverseTransformDirection(playerBody.velocity);

            if (XCI.GetButton(XboxButton.A, controller) && tempBoostTimer > 0 && isAlive)
            {
                isBoosting = true;
                if (!mainSource2.isPlaying) {
                    mainSource2.clip = boostSound;
                    mainSource2.loop = true;
                    mainSource2.volume = 0.8f;
                    mainSource2.Play();
                }
                tempBoostTimer -= Time.deltaTime;
                boostEffect.SetActive(true);
                cameraShake.BoostShake(boostShake);
                tempMaxSpeed = boostSpeed;
                if (speed < tempMaxSpeed)
                    playerBody.AddRelativeForce(Vector3.forward * boostPower, ForceMode.Force);
            }
            else
            {
                if(isBoosting)
                    cameraShake.StopBoostShake();
                
                if (mainSource2.isPlaying) {
                    mainSource2.loop = false;
                    mainSource2.volume = 0f;
                    mainSource2.Stop();
                }
                isBoosting = false;
                tempMaxSpeed = maxSpeed;
                boostEffect.SetActive(false);
                if (tempBoostTimer < boostTimer)
                    tempBoostTimer += Time.deltaTime / 4;
            }

            if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && isAlive || XCI.GetAxis(XboxAxis.LeftTrigger, controller) != 0 && isAlive)
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
                    Break(breakPower);
                else
                    return;
            }
            else if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0 && isAlive)
            {
                if (localVel.z < -0.01f)
                    Break(breakPower);
                else
                    Accelerate();
            }
            else if (XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0 && isAlive && !isBoosting)
            {
                if (localVel.z > 0.01f)
                    Break(breakPower);
                else
                    Reverse();
            }
            else
            {
                if (isGrounded)
                {
                    if (localVel.z > 7f || localVel.z < -7f)
                        playerBody.drag = declDrag / 2;

                    else if (localVel.z > 4f || localVel.z < -4f)
                        playerBody.drag = declDrag;

                    else
                        playerBody.drag = declDrag * 2;
                }
                else
                    playerBody.drag = 0;

                wheelColliders[0].motorTorque = 0;
                wheelColliders[1].motorTorque = 0;
                wheelColliders[2].motorTorque = 0;
                wheelColliders[3].motorTorque = 0;
            }

            //move wheel visuals
            WheelRotation();

            //Jump();
            LightTrails();
        }
        else
        {
            wheelColliders[0].steerAngle = 0;
			wheelColliders[1].steerAngle = 0;
			wheelColliders[2].steerAngle = 0;
			wheelColliders[3].steerAngle = 0;
			wheelColliders[0].steerAngle = 0;
			Break (breakPower);
            isBoosting = false;
            tempMaxSpeed = maxSpeed;
            boostEffect.SetActive(false);
            cameraShake.StopBoostShake();
            if (tempBoostTimer < boostTimer)
                tempBoostTimer = boostTimer;
        }

        if(deathTimerStart)
            deathTimer += Time.fixedDeltaTime;

        if (deathTimer >= 0.5f)
        {
            explosion.SetActive(false);
            explosionSphere.gameObject.SetActive(false);
            deathTimerStart = false;
            deathTimer = 0.0f;
        }
    }

    //=========================================SOUND=================================================
    void fadeIn(AudioSource _source) {
        if (audio2Volume < 0.5) {
            audio2Volume += 0.5f * Time.deltaTime;
            audioSource2.volume = audio2Volume;
        }
    }
 
    void fadeOut(AudioSource _source) {
        if(audio1Volume > 0.1)
        {
            audio1Volume -= 0.5f * Time.deltaTime;
            audioSource1.volume = audio1Volume;
        }
    }

    void CrossFade(AudioSource _sourceFrom, AudioSource _sourceTo, AudioClip clip)
    {
        if (_sourceFrom.isPlaying)
        {
            if (_sourceFrom.volume > 0.1f) {
                fadeOut(_sourceFrom);
            } else {
                _sourceFrom.volume = 0;
                _sourceFrom.loop = false;
                _sourceFrom.Stop();
                Debug.Log("Stoped");
            }
        }
        if (!_sourceTo.isPlaying) {
            _sourceTo.clip = clip;
            _sourceTo.loop = true;
            _sourceTo.Play();
            Debug.Log("Started");
        }
        fadeIn(_sourceTo);
    }

    void EngineSound()
    {
        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) == 0)
        {
            if (audioSource2.isPlaying) {
                CrossFade(audioSource2, audioSource1, idleSound);
            }
        } else {
            if (audioSource1.isPlaying) {
                CrossFade(audioSource1, audioSource2, accSoundHigh);
            }
        }
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
        bool skidStartFL = false;
        bool skidStartFR = false;
        bool skidStartRL = false;
        bool skidStartRR = false;

        bool minSkidSpeed = false;

        //Checks the speed to see if the car is below the threshold to spin out
        if (speed < startingSkidSpeed)
            minSkidSpeed = true;
        else
            minSkidSpeed = false;

        //Checks all 4 skidmark conditions and stores them in a bool
        bool accelSkid = (minSkidSpeed && (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0));
        bool driftSkid = (XCI.GetButton(XboxButton.X, controller) && XCI.GetAxis(XboxAxis.LeftStickX, controller) != 0);
        bool reverseSkid = (XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0) && (XCI.GetAxis(XboxAxis.LeftStickX, controller) != 0);
        bool brakeSkid = (!minSkidSpeed && XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0) && localVel.z > 0.01f;

        //Checks if any of the skidmark conditions were fulfilled, if they were, it commences the skidmark trail
        if (driftSkid && groundedFL)
            skidStartFL = true;
        if (driftSkid && groundedFR)
            skidStartFR = true;
        if ((accelSkid || driftSkid || reverseSkid || brakeSkid) && groundedRL)
            skidStartRL = true;
        if ((accelSkid || driftSkid || reverseSkid || brakeSkid) && groundedRR)
            skidStartRR = true;

        if (skidStartFL)
        {
            if (leftFrontSkidMark == null)
            {
                leftFrontSkidMark = Instantiate(skidMarkPrefab);
                leftFrontSkidMark.transform.parent = transform;
                leftFrontSkidMark.transform.localPosition = wheelColliders[0].transform.localPosition - Vector3.up * 0.45f;
            }
        }
        else if (leftFrontSkidMark != null)
        {
            leftFrontSkidMark.transform.parent = null;
            leftFrontSkidMark = null;
        }

        if (skidStartFR)
        {
            if (rightFrontSkidMark == null)
            {
                rightFrontSkidMark = Instantiate(skidMarkPrefab);
                rightFrontSkidMark.transform.parent = transform;
                rightFrontSkidMark.transform.localPosition = wheelColliders[3].transform.localPosition - Vector3.up * 0.45f;
            }
        }
        else if (rightFrontSkidMark != null)
        {
            rightFrontSkidMark.transform.parent = null;
            rightFrontSkidMark = null;
        }

        if (skidStartRL)
        {
            if (leftRearSkidMark == null)
            {
                leftRearSkidMark = Instantiate(skidMarkPrefab);
                leftRearSkidMark.transform.parent = transform;
                leftRearSkidMark.transform.localPosition = wheelColliders[1].transform.localPosition - Vector3.up * 0.45f;
            }
        }
        else if (leftRearSkidMark != null)
        {
            leftRearSkidMark.transform.parent = null;
            leftRearSkidMark = null;
        }

        if (skidStartRR)
        {
            if (rightRearSkidMark == null)
            {
                rightRearSkidMark = Instantiate(skidMarkPrefab);
                rightRearSkidMark.transform.parent = transform;
                rightRearSkidMark.transform.localPosition = wheelColliders[2].transform.localPosition - Vector3.up * 0.45f;
            }
        }
        else if (rightRearSkidMark != null)
        {
            rightRearSkidMark.transform.parent = null;
            rightRearSkidMark = null;
        }

        //Skid sound 
        if (skidStartFL || skidStartFR || skidStartRL || skidStartRR)
        {
            if(!mainSource3.isPlaying)
            {
                mainSource3.PlayOneShot(skidSounds[Random.Range(0,2)], 0.5f);
            }
        } else {
            mainSource3.Stop();
        }
    }

    void WheelRotation()
	{
		if (!driftbool)
		{
			wheels [0].localEulerAngles = new Vector3 (wheels [0].localEulerAngles.x, wheelColliders [0].steerAngle - wheels [0].localEulerAngles.z, wheels [0].localEulerAngles.z);
			wheels [1].localEulerAngles = new Vector3 (wheels [1].localEulerAngles.x, (wheelColliders [3].steerAngle - wheels [1].localEulerAngles.z) + 180, wheels [1].localEulerAngles.z);
		}
        else
		{
			wheels [0].localEulerAngles = new Vector3 (wheels [0].localEulerAngles.x, -(wheelColliders [1].steerAngle - wheels [0].localEulerAngles.z), wheels [0].localEulerAngles.z);
			wheels [1].localEulerAngles = new Vector3 (wheels [1].localEulerAngles.x, -((wheelColliders [2].steerAngle - wheels [1].localEulerAngles.z) + 180), wheels [1].localEulerAngles.z);
		}

		wheels[0].Rotate(wheelColliders[0].rpm / 60 * 360 * Time.deltaTime, 0, 0);
		wheels[1].Rotate(wheelColliders[3].rpm / 60 * 360 * Time.deltaTime, 0, 0);
		wheels[2].Rotate(wheelColliders[1].rpm / 60 * 360 * Time.deltaTime, 0, 0);
		wheels[3].Rotate(wheelColliders[2].rpm / 60 * 360 * Time.deltaTime, 0, 0);
	}

    void IsNewLeader()
    {
        if (playerID == Game_Manager.leaderboard[0].playerID && !Game_Manager.isDraw)
            leaderPosition.SetActive(true);
        else
            leaderPosition.SetActive(false);
    }
    #endregion

    //=========================================DAMAGE===============================================
    public void TakeDamage(float dmgAmount)
    {
        carHealth -= dmgAmount;
        StartCoroutine(DmgHueFade(0.3f));
        cameraShake.Shake(impactShake, impactShakeTime);

        if (carHealth < savedCarHealth/2)
        {
            isDamaged = true;
        }
        else
        {
            isDamaged = false;
        }

        if(carHealth <= 0)
		{
			isAlive = false;
            if (playerID == 1)
                Game_Manager.playerData[0].playerDeaths++;
            else if (playerID == 2)
                Game_Manager.playerData[1].playerDeaths++;
            else if (playerID == 3)
                Game_Manager.playerData[2].playerDeaths++;
            else if (playerID == 4)
                Game_Manager.playerData[3].playerDeaths++;

			wheelColliders[0].steerAngle = 0;
			wheelColliders[1].steerAngle = 0;
			wheelColliders[2].steerAngle = 0;
			wheelColliders[3].steerAngle = 0;
			wheelColliders[0].steerAngle = 0;
			Break (breakPower);
            isBoosting = false;
            tempMaxSpeed = maxSpeed;
            boostEffect.SetActive(false);
            cameraShake.StopBoostShake();
            if (tempBoostTimer < boostTimer)
                tempBoostTimer = boostTimer;

            for (int i = 0; i < carParts.Length; i++)
			{
				//carParts[i].GetComponent<Renderer> ().material.color = death;
                carParts[i].GetComponent<carPart>().alive = false;
			}

            mainSource1.PlayOneShot(explosionSound, 0.35f);
            explosion.SetActive(true);
            explosionSphere.gameObject.SetActive(true);
            explosionSphere.radius = 10;

            deathTimerStart = true;

            OnPlayerDead(playerID);
            StartCoroutine("GhostMode");
		}
    }

    IEnumerator GhostMode()
    {
        yield return new WaitForSeconds (3);
        camRig.SetActive(false);
        GameObject tempGhost = Instantiate(ghostCar, ghostSpawn.position, transform.localRotation);
        CarController tempGhostScript = tempGhost.GetComponent<CarController>();
        tempGhostScript.isGhost = true;
        tempGhostScript.controller = controller;
        enabled = false;
    }

    IEnumerator DmgHueFade(float time)
    {
        
       if (m_Profile == null)
         yield return null;

        VignetteModel.Settings vignette = m_Profile.vignette.settings;
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            vignette.color = Color.Lerp(vignette.color, new Color(0.8f, 0, 0), (elapsedTime / time));
            vignette.intensity = Mathf.Lerp(vignette.intensity, 0.452f, (elapsedTime / time));
            vignette.smoothness = Mathf.Lerp(vignette.smoothness, 0.513f, (elapsedTime / time));
            vignette.roundness = Mathf.Lerp(vignette.roundness, 1f, (elapsedTime / time));
            m_Profile.vignette.settings = vignette;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        vignette.color = new Color(0.6f, 0, 0);
        vignette.intensity = 0.452f;
        vignette.smoothness = 0.513f;
        vignette.roundness = 1f;
        m_Profile.vignette.settings = vignette;

        yield return new WaitForSeconds(0.15f);
        float outElapsedTime = 0;
        while (outElapsedTime < 1.5f)
        {
            vignette.color = Color.Lerp(vignette.color, new Color(0, 0, 0), (outElapsedTime / 2));
            vignette.intensity = Mathf.Lerp(vignette.intensity, 0.585f, (outElapsedTime / 2));
            vignette.smoothness = Mathf.Lerp(vignette.smoothness, 0.208f, (outElapsedTime / 2));
            vignette.roundness = Mathf.Lerp(vignette.roundness, 0.282f, (outElapsedTime / 2));
            m_Profile.vignette.settings = vignette;
            outElapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator HueFlash(float time)
    {
        
       if (m_Profile == null)
         yield return null;

        ColorGradingModel.Settings colorGrading = m_Profile.colorGrading.settings;
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            colorGrading.tonemapping.neutralWhiteLevel = Mathf.Lerp(colorGrading.tonemapping.neutralWhiteLevel, 1f, (elapsedTime / time));
            colorGrading.tonemapping.neutralWhiteIn = Mathf.Lerp(colorGrading.tonemapping.neutralWhiteLevel, 15f, (elapsedTime / time));
            m_Profile.colorGrading.settings = colorGrading;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        colorGrading.tonemapping.neutralWhiteLevel = Mathf.Lerp(colorGrading.tonemapping.neutralWhiteLevel, 1f, (elapsedTime / time));
        colorGrading.tonemapping.neutralWhiteIn = Mathf.Lerp(colorGrading.tonemapping.neutralWhiteLevel, 15f, (elapsedTime / time));
        m_Profile.colorGrading.settings = colorGrading;

        yield return new WaitForSeconds(0.1f);
        float outElapsedTime = 0;
        while (outElapsedTime < 1)
        {
            colorGrading.tonemapping.neutralWhiteLevel = Mathf.Lerp(colorGrading.tonemapping.neutralWhiteLevel, 5.3f, (elapsedTime / time));
            colorGrading.tonemapping.neutralWhiteIn = Mathf.Lerp(colorGrading.tonemapping.neutralWhiteLevel, 10f, (elapsedTime / time));
            m_Profile.colorGrading.settings = colorGrading;
            outElapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }


    public void CameraSetUp()
    {
        if (playerID == 1)
        {
            Debug.Log("Found Cam1");
            playerCam = GameObject.FindGameObjectWithTag("Cam1");
            playerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = playerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider1").GetComponent<Slider>();
        }
        else if (playerID == 2)
        {
            playerCam = GameObject.FindGameObjectWithTag("Cam2");
            playerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = playerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider2").GetComponent<Slider>();
        }
        else if (playerID == 3)
        {
            playerCam = GameObject.FindGameObjectWithTag("Cam3");
            playerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = playerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider3").GetComponent<Slider>();
        }
        else if (playerID == 4)
        {
            playerCam = GameObject.FindGameObjectWithTag("Cam4");
            playerCam.transform.parent.GetComponent<SmoothCamera>().NewPlayerSetUp(this.transform, cameraDesiredPosition, cameraFocus, cameraPivot, carFront);
            cameraShake = playerCam.GetComponent<CamShake>();
            boostSlider = GameObject.FindGameObjectWithTag("BoostSlider4").GetComponent<Slider>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ExplosionSphere")
        {
            StartCoroutine(DmgHueFade(0.3f));
            cameraShake.Shake(impactShake, impactShakeTime);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (speed >= minAttackSpeed && impactTimer <= 0)
        {
            if (other.gameObject.tag == "Player" || (other.gameObject.tag == "Ground" && (localVel.y <= -7 || localVel.y >= 7)))
            {
                if(speed >= 90) {mainSource1.PlayOneShot(impactSounds[Random.Range(3,4)], 0.35f);}
                else if (speed >= 60) {mainSource1.PlayOneShot(impactSounds[Random.Range(1,2)], 0.35f);}
                else {mainSource1.PlayOneShot(impactSounds[0], 0.35f);}
                cameraShake.Shake(impactShake, impactShakeTime);
                if (!isGhost)
                {
                    GameObject tempImpactEffect = Instantiate(impactEffect, other.contacts[0].point, Quaternion.identity);
                }
                impactTimer = hitImpactTimer;
            }
        } else if (speed >= 10 && impactTimer <= 0) {
                mainSource1.PlayOneShot(impactSounds[0], 0.35f);
        }
    }
}
