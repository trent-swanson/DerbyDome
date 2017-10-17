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

    private void BodyScore()
    {
        if (rigidBody.transform.parent.name == "Car1")
            Score.Player1ScoreActual += (int)damageTaken;
        if (rigidBody.transform.parent.name == "Car2")
            Score.Player2ScoreActual += (int)damageTaken;
        if (rigidBody.transform.parent.name == "Car3")
            Score.Player3ScoreActual += (int)damageTaken;
        if (rigidBody.transform.parent.name == "Car4")
            Score.Player4ScoreActual += (int)damageTaken;
    }

    private void BumperScore()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (carVelo >= minAttackVelocity)
        {
            //The Bumper v Bumper Doesnt trigger - Ryan
            //Calculation occurs with head on collision with another car
            if (other.gameObject.tag == "Front Bumper")
            {
                other.gameObject.GetComponent<CarController>().carHealth -= (damageTaken / 2);
                //Gets a copy of the damage dealt for the Score.CS NOT NOT DELETE
                Debug.Log("bumper hit");
                Debug.Log(transform.parent.GetComponent<CarController>().playerID + ":  " + other.gameObject.GetComponent<CarController>().carHealth);
                return;
            }

            //Calculation occurs when a player hits anywhere on the other car except for the bumper
            else if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<CarController>().carHealth -= damageTaken;
                //Gets a copy of the damage dealt for the Score.CS NOT NOT DELETE
                BodyScore();
                Debug.Log(transform.parent.GetComponent<CarController>().playerID + ":  " + other.gameObject.GetComponent<CarController>().carHealth);
                return;
            }
        }
    }


}