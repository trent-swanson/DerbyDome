using System;
using UnityEngine;
using XboxCtrlrInput;

public class DriftCamera : MonoBehaviour
{
    public XboxController controller;
    [Serializable]

    public class AdvancedOptions
    {
        public bool updateCameraInUpdate;
        public bool updateCameraInFixedUpdate;
        public bool updateCameraInLateUpdate = true;
        //public KeyCode switchRightViewKey = KeyCode.RightArrow;
        public KeyCode switchCentreViewKey = KeyCode.Space;
    }
    [Space]

    // The cameras desired position. It should follow this point around smoothly. 
    public Transform cameraDesiredPosition;
    // cameraFocus looks at carFrontFocusPoint or the center of the map, only rotates on Y axis.
    public Transform cameraFocus;
    // cameraPivot rotates on player input.
    public Transform cameraPivot;
    // The front of the car. This exists so that the pivot can use this point as a default LookAt() target. 
    // By default the pivot is aimed here.
    public Transform carFront;
    // The centre of the map. The camera can be toggled to look at this.
    // This causes the pivotTarget to LookAt() the centre of the map.
    public Transform mapCentre;
    //// If the player wants to look at the side of their car they can.
    //// The camera will shift its position and rotation to match this transform.
    //public Transform sideView;

    [Space]

    public AdvancedOptions advancedOptions;

    [Space]

    //bool m_ShowingRightSideView;
    public bool showingCentreView = false;

    [Space]

    public float mouseX = 0.0f;
    public float mouseY = 0.0f;
    public float ctrlX = 0.0f; 
    public float ctrlY = 0.0f;
    public float finalInputX = 0.0f;
    public float finalInputY = 0.0f;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public float smoothing = 6f; // How fast the camera lerps

    // Smoothly transitions from normal view to centre view. The smoothing causes the camera to lag behind the player though.
    public void ViewCentre()
    {
        // rotates cameraPivot to look at the centre of the map.
        cameraFocus.transform.LookAt(mapCentre);

        // rotates and moves the camera. Smoothly lerps between the cameras current transform to the desired transform.
        transform.position = Vector3.Lerp(transform.position, cameraDesiredPosition.position, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraDesiredPosition.rotation, Time.deltaTime * smoothing);
    }

    // Smoothly transitions from centre view to normal view. The smoothing causes the camera to lag behind the player though.
    public void ViewFront()
    {
        // rotates cameraPivot to look at the front of the car.
          cameraFocus.transform.LookAt(carFront);

        // rotates and moves the camera. Smoothly lerps between the cameras current transform to the desired transform.
        transform.position = Vector3.Lerp(transform.position, cameraDesiredPosition.position, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraDesiredPosition.rotation, Time.deltaTime * smoothing);
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (advancedOptions.updateCameraInFixedUpdate)
            UpdateCamera();
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        ctrlX = XCI.GetAxis(XboxAxis.RightStickX);
        ctrlY = XCI.GetAxis(XboxAxis.RightStickY);
        finalInputX = mouseX + ctrlX;
        finalInputY = mouseY + ctrlY;
        //Debug.Log("\nfinalInputX: ");
        //Debug.Log(finalInputX);
        // Debug.Log("\nfinalInputY: ");
        //Debug.Log(finalInputY);

        //if (Input.GetKeyDown(advancedOptions.switchRightViewKey))
        //    m_ShowingRightSideView = !m_ShowingRightSideView;
        if (Input.GetKeyDown(advancedOptions.switchCentreViewKey))
            showingCentreView = !showingCentreView;

        if (XCI.GetButtonDown(XboxButton.Y, controller))
            showingCentreView = !showingCentreView;

        if (advancedOptions.updateCameraInUpdate)
            UpdateCamera();
    }

    private void LateUpdate()
    {
        if (advancedOptions.updateCameraInLateUpdate)
            UpdateCamera();
    }

    private void UpdateCamera()
    {

        // Allows swapping between different camera views.
        if (showingCentreView)
        {
            ViewCentre();
        }
        else
        {
            ViewFront();
        }

       
        transform.LookAt(cameraPivot);
        cameraPivot.Rotate(-finalInputY, finalInputX, 0);
        

        //Quaternion targetRotation = Quaternion.Euler(currentY, currentX, 0);

        //cameraPivot.position = cameraPivot.position + targetRotation;


    }
}
