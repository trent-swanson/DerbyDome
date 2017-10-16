using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    Rigidbody rigidBody;

    public float attackValue = 100.0f;
    //change this positivly to increase DMG
    public float still = 1;
    public float damageTaken;
    public float carVelo;
    public int minAttackVelocity = 6;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        carVelo = transform.parent.GetComponent<CarController>().localVel.z;
        damageTaken = (attackValue + (carVelo * 10));
    }

    void CarDamage()
    {

    }


    void OnCollisionEnter(Collision other)
    {
        if (carVelo >= minAttackVelocity)
        {
            //Calculation occurs with head on collision with another car
            if (other.gameObject.tag == "Front Bumper")
            {
                other.gameObject.GetComponent<CarController>().carHealth -= (damageTaken / 2);
                Debug.Log("bumper hit");
                Debug.Log(transform.parent.GetComponent<CarController>().playerID + ":  " + other.gameObject.GetComponent<CarController>().carHealth);
                return;
            }

            //Calculation occurs when a player hits anywhere on the other car except for the bumper
            else if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<CarController>().carHealth -= damageTaken;
                Debug.Log(transform.parent.GetComponent<CarController>().playerID + ":  " + other.gameObject.GetComponent<CarController>().carHealth);
                return;
            }
        }
    }


}