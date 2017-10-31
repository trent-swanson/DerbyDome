using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// This script is attached to every players car. It contains functions for activating each ability.
public class PowerManager : MonoBehaviour
{
    public GameObject ShieldParent;
    public GameObject BombParent;
    public GameObject Shield;
    private GameObject newShield;
    public GameObject Bomb;
    private GameObject newBomb;

    public int activePowerUp = 0;
    private float timer;

    //Bomb paramaters
    private float power = 100000.0f;
    private float radius = 10.0f;
    private float upForce = 1000.0f;

    // Use this for initialization
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pickup_ShockWave_001")
            activePowerUp = 1;
        if (other.gameObject.name == "Pickup_EMPBlast_001")
            activePowerUp = 2;
        if (other.gameObject.name == "Pickup_Shield_001")
            activePowerUp = 3;
        if (other.gameObject.name == "Pickup_Daruma_001")
            activePowerUp = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (activePowerUp > 0)
            timer += Time.deltaTime;

        if (XCI.GetButtonDown(XboxButton.B))
        {
            switch (activePowerUp)
            {
                case 0:
                    Debug.Log("No power.");
                    break;
                case 1:
                    // Activate Shock power.
                    Debug.Log("Activated Shock power.");
                    activePowerUp = 0;
                    break;
                case 2:
                    // Activate EMP power.
                    Debug.Log("Activated EMP power.");
                    activePowerUp = 0;
                    break;
                case 3:
                    // Activate the shield power.
                    Debug.Log("Activated shield power.");
                    if (ShieldParent.transform.childCount == 0)
                    {
                        timer = 0;
                        newShield = Instantiate(Shield) as GameObject;
                        newShield.transform.SetParent(ShieldParent.transform, false);
                    }
                    break;
                case 4:
                    //Activate the Bomb power
                    Debug.Log("Activated Bomb power");
                    if (BombParent.transform.childCount == 0)
                    {
                        timer = 0;
                        newBomb = Instantiate(Bomb) as GameObject;
                        newBomb.transform.SetParent(BombParent.transform, false);
                    }
                    break;
                default:
                    Debug.Log("Invalid.");
                    break;
            }
        }
        //Shield effect lendge
        if (timer > 20 && activePowerUp == 3)
        {
            Destroy(newShield.gameObject);
            activePowerUp = 0;
            timer = 0;
        }
        //Bomb timer before detonation
        if (timer > 2 && activePowerUp == 4)
        {
            Vector3 ExplostionPosition = newBomb.transform.position;
            //Grabs all colliders within radius
            Collider[] colliders = Physics.OverlapSphere(ExplostionPosition, radius);
            foreach(Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                //Checks if the grabed colliders have Rigidbodys
                //Adds force to the Grabed Colliders on line 101
                if (rb != null)
                rb.AddExplosionForce(power, ExplostionPosition, radius, upForce, ForceMode.Impulse);
            }
            Destroy(newBomb.gameObject);
            activePowerUp = 0;
            timer = 0;
        }
    }
}
