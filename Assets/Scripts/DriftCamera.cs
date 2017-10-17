//================================================================================
//DriftCamera
//
//Purpose:  To control the camera and to allow that player to control
//the angle in which they are viewing the car from
//
//Creator: Andrew Hand
//Edited by: Joel Goodchild
//================================================================================

using System;
using UnityEngine;
using XboxCtrlrInput;

public class DriftCamera : MonoBehaviour
{
    public XboxController controller;
    
    [Space]

    // The cameras desired position. It should follow this point around smoothly.
    public Transform cameraDesiredPosition;

    // cameraPivot is what the camera looks at. rotates on player input.
    public Transform cameraPivot;

    // The front of the car. This exists so that the pivot can use this point as a default LookAt() target.
    // By default the pivot is aimed here.
    public Transform carFront;

    // The centre of the map. The camera can be toggled to look at this.
    // This causes the pivotTarget to LookAt() the centre of the map.
    public Transform mapCentre;

    [Space]

    public bool showingCentreView = false;

    [Space]

    [Tooltip("Lowest point in which the camera can go.")]
    [Range(360.0f, 340.0f)]
    public float minClampX = 350.0f;

    [Tooltip("Highest point in which the camera can go.")]
    [Range(0.0f, 40.0f)]
    public float maxClampX = 20.0f;

    [Tooltip("Maximum angle that the camera can be rotated around the right of the car.")]
    [Range(360.0f, 180.01f)]
    public float minClampY = 210.0f;

    [Tooltip("Maximum angle that the camera can be rotated around the left of the car.")]
    [Range(0.0f, 179.99f)]
    public float maxClampY = 150.0f;

    [Tooltip("How fast the camera moves with the controllers input.")]
    [Range(1.0f, 10.0f)]
    public float inputSensitivity = 2.0f;

    [Tooltip("The speed at which the camera lerps.")]
    public float smoothing = 6f;

    [Serializable]
    public class AdvancedOptions
    {
        public bool isInUpdate = false;
        public bool isInFixedUpdate = true;
        public bool isInLateUpdate = false;
        public KeyCode switchCentreViewKey = KeyCode.Space;
    }

    [Space]

    public AdvancedOptions advancedOptions;

    private float ctrlX = 0.0f;
    private float ctrlY = 0.0f;
    private float finalInputX = 0.0f;
    private float finalInputY = 0.0f;
    private float rotX = 0.0f;
    private float rotY = 0.0f;


    // Smoothly transitions from centre view to normal view. The smoothing causes the camera to lag behind the player though.
    public void ViewFront()
    {
        // rotates cameraPivot to look at the front of the car.
        cameraPivot.transform.LookAt(carFront);

        // rotates and moves the camera. Smoothly lerps between the cameras current transform to the desired transform.
        transform.position = Vector3.Lerp(transform.position, cameraDesiredPosition.position, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraDesiredPosition.rotation, Time.deltaTime * smoothing);
    }
    // Smoothly transitions from normal view to centre view. The smoothing causes the camera to lag behind the player though.
    public void ViewCentre()
    {
        // rotates cameraPivot to look at the centre of the map.
        cameraPivot.transform.LookAt(mapCentre);

        // rotates and moves the camera. Smoothly lerps between the cameras current transform to the desired transform.
        transform.position = Vector3.Lerp(transform.position, cameraDesiredPosition.position, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraDesiredPosition.rotation, Time.deltaTime * smoothing);
    }


    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    private void FixedUpdate()
    {
        if (advancedOptions.isInFixedUpdate)
        { UpdateCamera(); }
    }

    private void Update()
    {
        if (Input.GetKeyDown(advancedOptions.switchCentreViewKey))
            showingCentreView = !showingCentreView;

        if (XCI.GetButtonDown(XboxButton.Y, controller))
            showingCentreView = !showingCentreView;

        //UpdateCamera();
        if (advancedOptions.isInUpdate)
        { UpdateCamera(); }
    }

    private void LateUpdate()
    {
        if (advancedOptions.isInLateUpdate)
        { UpdateCamera(); }
    }

    private void UpdateCamera()
    {
        ctrlX = XCI.GetAxis(XboxAxis.RightStickX, controller);
        ctrlY = XCI.GetAxis(XboxAxis.RightStickY, controller);

        //Ensures that if the player is not currently moving the camera
        //it snaps back to the default rear to front view of the car
        if (ctrlX == 0 && ctrlY == 0)
            ViewFront();

        finalInputX = ctrlX * inputSensitivity;
        finalInputY = ctrlY * inputSensitivity;

        // Preserves orientation.
        transform.LookAt(cameraPivot);
        
        cameraPivot.Rotate(-finalInputY, finalInputX, 0);

        Vector3 euler = cameraPivot.localEulerAngles;
        //================================================================================
        //Limits the x rotation of the camera
        if (euler.x > maxClampX && euler.x < 180.0f)
        {
            euler.x = maxClampX;
            cameraPivot.localEulerAngles = euler;
        }

        if (euler.x < minClampX && euler.x >= 180.0f)
        {
            euler.x = minClampX;
            cameraPivot.localEulerAngles = euler;
        }
        //================================================================================
        //Limits the y rotation of the camera
        if (euler.y > maxClampY && euler.y < 180.0f)
        {
            euler.y = maxClampY;
            cameraPivot.localEulerAngles = euler;
        }

        if (euler.y < minClampY && euler.y >= 180.0f)
        {
            euler.y = minClampY;
            cameraPivot.localEulerAngles = euler;
        }
        //================================================================================

        transform.position = Vector3.Lerp(transform.position, cameraDesiredPosition.position, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraDesiredPosition.rotation, Time.deltaTime * smoothing);

        if (transform.position.y <= 0.2f)
        {
            transform.position.Set(transform.position.x, 0.2f, transform.position.z);
        }
    }
}
