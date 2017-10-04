using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// This script is attached to every players car. It contains functions for activating each ability.
public class PowerManager : MonoBehaviour
{
    [HideInInspector]
    public XboxController tempController;
    public enum Powers { None, Bomb, EMP, Shield };
    public int activePower = (int)Powers.None;

    // Use this for initialization
    void Start()
    {
        tempController = GetComponent<CarController>().controller;
    }

    // Update is called once per frame
    void Update()
    {
        if (XCI.GetButtonDown(XboxButton.A, tempController))
        {
            switch (activePower)
            {
                case 0:
                    Debug.Log("No power.");
                    break;
                case 1:
                    // Activate bomb power.
                    // 
                    Debug.Log("Activated bomb power.");
                    break;
                case 2:
                    // Activate EMP power.
                    Debug.Log("Activated EMP power.");
                    break;
                case 3:
                    // Activate the shield power.
                    Debug.Log("Activated shield power.");
                    break;
                default:
                    Debug.Log("Invalid.");
                    break;
            }    
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            switch (activePower)
            {
                case 0:
                    Debug.Log("No power.");
                    break;
                case 1:
                    // Activate bomb power.
                    Debug.Log("Activated bomb power.");
                    break;
                case 2:
                    // Activate EMP power.
                    Debug.Log("Activated EMP power.");
                    break;
                case 3:
                    // Activate the shield power.
                    Debug.Log("Activated shield power.");
                    break;
                default:
                    Debug.Log("Invalid.");
                    break;
            }

        }
    }
}
