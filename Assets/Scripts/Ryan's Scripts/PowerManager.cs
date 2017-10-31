using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// This script is attached to every players car. It contains functions for activating each ability.
public class PowerManager : MonoBehaviour
{
    public GameObject ParentObj;
    public GameObject Shield;
    private GameObject newShield;
    public GameObject Bomb;
    public int activePower = 0;
    private float timer;
    // Use this for initialization
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pickup_ShockWave_001")
            activePower = 1;
        if (other.gameObject.name == "Pickup_EMPBlast_001")
            activePower = 2;
        if (other.gameObject.name == "Pickup_Shield_001")
            activePower = 3;
        if (other.gameObject.name == "Pickup_Daruma_001")
            activePower = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (activePower > 0)
            timer += Time.deltaTime;

        if (XCI.GetButtonDown(XboxButton.B))
        {
            switch (activePower)
            {
                case 0:
                    Debug.Log("No power.");
                    break;
                case 1:
                    // Activate Shock power.
                    Debug.Log("Activated Shock power.");
                    activePower = 0;
                    break;
                case 2:
                    // Activate EMP power.
                    Debug.Log("Activated EMP power.");
                    activePower = 0;
                    break;
                case 3:
                    // Activate the shield power.
                    Debug.Log("Activated shield power.");
                    if (ParentObj.transform.childCount == 0)
                    {
                        timer = 0;
                        newShield = Instantiate(Shield) as GameObject;
                        newShield.transform.SetParent(ParentObj.transform, false);
                    }
                    break;
                case 4:
                    //Activate the Bomb power
                    Debug.Log("Activated Bomb power");
                    activePower = 0;
                    break;
                default:
                    Debug.Log("Invalid.");
                    break;
            }
        }
        if (timer > 20)
        {
            Destroy(newShield.gameObject);
            activePower = 0;
            timer = 0;
        }
    }
}
