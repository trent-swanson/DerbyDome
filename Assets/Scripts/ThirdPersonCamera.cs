﻿using System;
using UnityEngine;
using XboxCtrlrInput;


// This script gets attached to the car. You dragndrop a camera into the "cam" slot.
public class ThirdPersonCamera : MonoBehaviour
{
    [Serializable]
    public class AdvancedOptions
    {
        public bool updateCameraInUpdate;
        public bool updateCameraInFixedUpdate;
        public bool updateCameraInLateUpdate = true;
        public KeyCode switchCentreViewKey = KeyCode.Space;
    }

    [Space]

    public Camera cam;

    // The point the camera looks at.
    // Technically, the camera object will LookAt() the pivotTarget which will then LookAt() the lookAtTarget().
    // Should switch between the carFront and the mapCentre.
    [HideInInspector]
    public Transform lookAtTarget;


    public float smoothing = 6f; // How fast the camera lerps

    [Space]

    // The camera gets put here by default.
    public Transform cameraDefaultPosition;
    // The pivot the camera looks at and rotates around. This is so that the camera is always centered around the car.
    public Transform cameraPivot;
    // The front of the car. This exists so that the camera can use this point as a default LookAt() target. 
    // By default the camera looks here.
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

    // Smoothly transitions from normal view to centre view. The smoothing causes the camera to lag behind the player though.
    public void TransitionCamera(Transform a_target)
    {
        transform.position = Vector3.Lerp(transform.position, a_target.position, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, a_target.rotation, Time.deltaTime * smoothing);
    }

    void Start()
    {
        // rotates the cameraDefaultPosition towards the pivot point.
        cameraDefaultPosition.LookAt(cameraPivot);
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
        // This if statement can be copied and new versions can be implemented to
        // provide the player with the ability to view their car from different angles
        // or view different objects and events in the map.
        if (showingCentreView)
        {
            //// Puts the camera next to the car, showing its side.
            //transform.position = Vector3.Lerp(transform.position, mapCentre.position, Time.deltaTime * smoothing);
            //transform.rotation = Quaternion.Slerp(transform.rotation, mapCentre.rotation, Time.deltaTime * smoothing);

            // rotates cameraPivot to look at the centre of the map.
            cameraPivot.transform.LookAt(mapCentre);

            TransitionCamera(cameraDefaultPosition);


        }
        else
        {
            // rotates cameraPivot to look at the front of the car.
            cameraPivot.transform.LookAt(carFront);

            TransitionCamera(cameraDefaultPosition);
        }


    }
}
