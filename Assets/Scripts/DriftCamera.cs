using System;
using UnityEngine;
using XboxCtrlrInput;

public class DriftCamera : MonoBehaviour
{
    public XboxController controller;
    [Serializable]

    public class AdvancedOptions
    {
        public bool isInUpdate = false;
        public bool isInFixedUpdate = true;
        public bool isInLateUpdate = false;
        public KeyCode switchCentreViewKey = KeyCode.Space;
    }
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

    public AdvancedOptions advancedOptions;

    [Space]

    public bool showingCentreView = false;

    [Space]

    public float mouseX = 0.0f;
    public float mouseY = 0.0f;
    public float ctrlX = 0.0f;
    public float ctrlY = 0.0f;
    public float finalInputX = 0.0f;
    public float finalInputY = 0.0f;
    public float maxClampX = 150.0f;
    public float minClampX = -150.0f;
    public float maxClampY = 35.0f;
    public float minClampY = -20.0f;
    public float inputSensitivity = 2.0f;
    public float smoothing = 6f; // How fast the camera lerps

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
        inputSensitivity = 2.0f;

    }

    private void FixedUpdate()
    {
        if (advancedOptions.isInFixedUpdate)
        { UpdateCamera(); }
    }

    private void Update()
    {
        
        /*Debug.Log("\nfinalInputX: ");
        Debug.Log(finalInputX);
        Debug.Log("\nfinalInputY: ");
        Debug.Log(finalInputY);
        */



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

        // Allows swapping between different camera views.
        /*if (showingCentreView)
        {
            ViewCentre();
        }
        else
        {
            ViewFront();
        }*/

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        ctrlX = XCI.GetAxis(XboxAxis.RightStickX, controller);
        ctrlY = XCI.GetAxis(XboxAxis.RightStickY, controller);
        finalInputX = (mouseX + ctrlX) * inputSensitivity;
        finalInputY = (mouseY + ctrlY) * inputSensitivity;

        // Preserves orientation.
        transform.LookAt(cameraPivot);

        cameraPivot.Rotate(-finalInputY, finalInputX, 0);

        transform.position = Vector3.Lerp(transform.position, cameraDesiredPosition.position, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraDesiredPosition.rotation, Time.deltaTime * smoothing);


        //Quaternion targetRotation = Quaternion.Euler(currentY, currentX, 0);

        //cameraPivot.position = cameraPivot.position + targetRotation;


    }
}
