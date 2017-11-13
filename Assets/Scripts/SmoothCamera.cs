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

public class SmoothCamera : MonoBehaviour
{
    public XboxController controller;
    
    [Space]

    private Transform player;
    private Transform cameraDesiredPosition;
    private Transform cameraFocus;
    private Transform cameraPivot;
    private Transform carFront;
    public Transform mapCentre;
    private Transform leader;
    public int playerNum;

    bool showingCentreView = false;

    [Space]
    [Space]
    [Tooltip("Highest point in which the camera can go.")]
    [Range(0, 150)]
    public float maxClampX = 150;

    [Tooltip("Maximum angle that the camera can be rotated around the right of the car.")]
    [Range(1, 150)]
    public float minClampY = 210.0f;

    [Tooltip("Maximum angle that the camera can be rotated around the left of the car.")]
    [Range(1, 150)]
    public float maxClampY = 150.0f;

    [Tooltip("The speed at which the camera lerps.")]
    public float smoothing = 6f;
    public float rotSmoothing = 6f;

    private float ctrlX = 0.0f;
    private float ctrlY = 0.0f;
    private float finalInputX = 0.0f;
    private float finalInputY = 0.0f;
    private bool isLeader = false;

    void OnEnable()
    {
        Score.OnLateUpdatePlayerLeader += NewLeader;
    }
    
    
    void OnDisable()
    {
        Score.OnLateUpdatePlayerLeader -= NewLeader;
    }

    void Start()
    {
        if (Game_Manager.roundCount == 0)
            isLeader = true;
    }
    
    private void FixedUpdate()
    {
        if (XCI.GetButtonDown(XboxButton.Y, controller))
            CenterToggle();

        CamFocus();
        UpdateCamera();
    }

    void SmoothFollow()
    {
        // rotates and moves the camera. Smoothly lerps between the cameras current transform to the desired transform.
        transform.position = Vector3.Slerp(transform.position, cameraDesiredPosition.position, Time.deltaTime * smoothing);
        //transform.rotation = Quaternion.Slerp(transform.rotation, cameraDesiredPosition.rotation, Time.deltaTime * smoothing);
    }

    void CenterToggle()
    {
        if (!showingCentreView)
        {
            showingCentreView = true;
           
        } else
        {
            showingCentreView = false;
        }        
    }

    void NewLeader()
    {
        if (Game_Manager.leaderboard[0].playerID == playerNum)
        {
            isLeader = true;
            showingCentreView = false;
        } else
        {
            leader = GameObject.FindGameObjectWithTag("Leader").transform;
            isLeader = false;
        }
    }

    void CamFocus()
    {
        if (!showingCentreView)
        {
            cameraFocus.transform.LookAt(carFront);
            SmoothFollow();
           
        } else
        {
            if(isLeader) {
                cameraFocus.transform.LookAt(mapCentre);
            } else {
                cameraFocus.transform.LookAt(leader);
            }
            SmoothFollow();
        }  
    }

    private void UpdateCamera()
    {
        ctrlX = XCI.GetAxis(XboxAxis.RightStickX, controller);
        ctrlY = XCI.GetAxis(XboxAxis.RightStickY, controller);

        finalInputX = ctrlX * 150;
        finalInputY = ctrlY * 150;
        finalInputX = Mathf.Clamp(finalInputX, -maxClampX, maxClampX);
        finalInputY = Mathf.Clamp(finalInputY, -minClampY, maxClampY);

        // Preserves orientation.
        transform.LookAt(cameraPivot);

        Quaternion camRotation = Quaternion.Euler(finalInputY, finalInputX, 0);
        //cameraPivot.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, camRotation, Time.deltaTime * rotSmoothing);
        cameraPivot.localRotation = Quaternion.Slerp(cameraPivot.localRotation, camRotation, Time.deltaTime * rotSmoothing);

        SmoothFollow();
    }

    public void NewPlayerSetUp(Transform _player, Transform _cameraDesiredPosition, Transform _cameraFocus, Transform _cameraPivot, Transform _carFront)
    {
        player = _player;
        cameraDesiredPosition = _cameraDesiredPosition;
        cameraFocus = _cameraFocus;
        cameraPivot = _cameraPivot;
        carFront = _carFront;
    }
}
