using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    Rigidbody rigidBody;

    public float attackValue = 100.0f;
    public float damageToTake;
    public float carSpeed;
    public float minAttackSpeed = 20;
    private Score scoreScript;
    private CarController carController;
    public float hitTimer = 3f;
    public float timer;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        scoreScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Score>();
        carController = transform.parent.GetComponent<CarController>();
        timer = 0;
    }

    void Update()
    {
        carSpeed = carController.speed;
        damageToTake = attackValue + carSpeed;

        if(timer > 0) {
            timer -= Time.deltaTime;
        }
    }

    private void BodyScore()
    {
        scoreScript.ScoreIncrease(carController.playerID, (int)damageToTake);
    }

    private void BumperScore()
    {
        scoreScript.ScoreIncrease(carController.playerID, (int)damageToTake / 2);
    }

    private void KillScore()
    {
        scoreScript.ScoreIncrease(carController.playerID, 300);
    }

    void OnTriggerEnter(Collider other)
    {
        if (carSpeed >= minAttackSpeed && timer <= 0)
        {
            //Calculation occurs with head on collision with another car
            /*/if (other.gameObject.tag == "FrontBumper")
            {
                other.transform.parent.gameObject.GetComponent<CarController>().TakeDamage(damageToTake / 2);
                Debug.Log("BumperHit!!!!!!");
                BumperScore();
                Debug.Log(transform.parent.GetComponent<CarController>().playerID + ":  " + other.gameObject.GetComponent<CarController>().carHealth); 
                timer = hitTimer;               
                return;
            }*/
            //Calculation occurs when a player hits anywhere on the other car except for the bumper
            if (other.gameObject.tag == "Player" && other.GetComponent<CarController>().isAlive)
            {
                other.gameObject.GetComponent<CarController>().TakeDamage(damageToTake);
                Debug.Log("body hit");
                BodyScore();
                if(other.gameObject.GetComponent<CarController>().isAlive == false)
                {
                    scoreScript.killIncrease(carController.playerID);
                    KillScore();
                }
                Debug.Log("Player" + other.gameObject.GetComponent<CarController>().playerID + ":  " + other.gameObject.GetComponent<CarController>().carHealth);
                timer = hitTimer;
                return;
            }
        }
    }
}