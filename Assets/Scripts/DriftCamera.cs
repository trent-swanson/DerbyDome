using System;
using UnityEngine;
using XboxCtrlrInput;

public class DriftCamera : MonoBehaviour
{
    [Serializable]
    public class AdvancedOptions
    {
        public bool updateCameraInUpdate;
        public bool updateCameraInFixedUpdate = true;
        public bool updateCameraInLateUpdate;
        //public KeyCode switchRightViewKey = KeyCode.RightArrow;
        public KeyCode switchCentreViewKey = KeyCode.Space;
    }
    
    [Space]

    public float smoothing = 6f; // How fast the camera lerps

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

    public float currentX = 0.0f;
    public float currentY = 0.0f;



    public void ManualControl()
    {

    }

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
        //if (Input.GetKeyDown(advancedOptions.switchRightViewKey))
        //    m_ShowingRightSideView = !m_ShowingRightSideView;
        if (Input.GetKeyDown(advancedOptions.switchCentreViewKey))
            showingCentreView = !showingCentreView;

        if (XCI.GetButtonDown(XboxButton.Y))
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

        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        //Quaternion targetRotation = Quaternion.Euler(currentY, currentX, 0);

        //cameraPivot.position = cameraPivot.position + targetRotation;


    }
}
