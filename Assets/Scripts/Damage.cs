using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    Rigidbody playerBody;
    private MultiTag PlayerTag;
    private BumperTag BumperTag;

    public double PlayerHealth1 = 1500;
    public double PlayerHealth2 = 1500;
    public double PlayerHealth3 = 1500;
    public double PlayerHealth4 = 1500;

    public float attackValue = 100.0f;
    //change this positivly to increase DMG
    public float AttackValue = 100.0f;
    public float DamageTaken = 1;
    public int NotMoving = 5;
    private float sitStillDamage = 1.5f;
    public float SitStillAttack = 1.5f;

    float timer;
    bool bumperDamage = false;

    // Use this for initialization
    void Start()
    {
        playerBody = GetComponentInParent<Rigidbody>();
        //setters for moving DMG and Stable DMG
        attackValue = AttackValue;
        sitStillDamage = SitStillAttack;

        PlayerTag = GetComponentInParent<MultiTag>();
        BumperTag = GetComponent<BumperTag>();
    }

    // Update is called once per frame
    void Update()
    {
        //To make sure the bumper v bumper damage only runs once
        timer += Time.deltaTime;
    }

    public double Bumperdamage()
    {
        if (BumperTag.Player1bumper == true)
        {
            PlayerHealth1 -= DamageTaken;
            bumperDamage = true;
            return PlayerHealth1;
        }
        if (BumperTag.Player2bumper == true)
        {
            PlayerHealth2 -= DamageTaken;
            bumperDamage = true;
            return PlayerHealth2;
        }
        if (BumperTag.Player3bumper == true)
        {
            PlayerHealth3 -= DamageTaken;
            bumperDamage = true;
            return PlayerHealth3;
        }
        if (BumperTag.Player4bumper == true)
        {
            PlayerHealth4 -= DamageTaken;
            bumperDamage = true;
            return PlayerHealth4;
        }
        else
        {
            return 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            //Body Damage
            //The otherPlayer below checks the Attacking player
            if (other.gameObject.tag == "Player" && bumperDamage == false)
            {
                //You take damage
                if (other.GetComponent<Rigidbody>().velocity.magnitude < playerBody.velocity.magnitude)
                {
                    if (other.GetComponent<Rigidbody>().velocity.magnitude < playerBody.velocity.magnitude * (float)0.80f)
                    {
                        if (other.GetComponent<Rigidbody>().velocity.magnitude < NotMoving)
                        {
                            DamageTaken = (attackValue + playerBody.velocity.magnitude) * sitStillDamage;
                            //This function determins who takes the damage
                            if (bumperDamage == false)
                            {
                                if (other.GetComponent<MultiTag>().Player1Tag == true)
                                    PlayerHealth1 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player2Tag == true)
                                    PlayerHealth2 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player3Tag == true)
                                    PlayerHealth3 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player4Tag == true)
                                    PlayerHealth4 -= DamageTaken;
                            }
                        }
                        else
                        {
                            DamageTaken = (attackValue + playerBody.velocity.magnitude) * (float)1.30f;
                            //This function determins who takes the damage
                            if (bumperDamage == false)
                            {
                                if (other.GetComponent<MultiTag>().Player1Tag == true)
                                    PlayerHealth1 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player2Tag == true)
                                    PlayerHealth2 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player3Tag == true)
                                    PlayerHealth3 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player4Tag == true)
                                    PlayerHealth4 -= DamageTaken;
                            }
                        }
                    }
                }
                //Target take damage
                else
                {
                    if ((other.GetComponent<Rigidbody>().velocity.magnitude * (float)0.80f) > playerBody.velocity.magnitude)
                    {
                        if (playerBody.velocity.magnitude < NotMoving)
                        {
                            DamageTaken = (attackValue + playerBody.velocity.magnitude) * sitStillDamage;
                            //This function determins who takes the damage
                            if (bumperDamage == false)
                            {
                                if (other.GetComponent<MultiTag>().Player1Tag == true)
                                    PlayerHealth1 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player2Tag == true)
                                    PlayerHealth2 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player3Tag == true)
                                    PlayerHealth3 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player4Tag == true)
                                    PlayerHealth4 -= DamageTaken;
                            }
                        }
                        else
                        {
                            DamageTaken = (attackValue + playerBody.velocity.magnitude) * (float)1.30f;
                            //This function determins who takes the damage
                            if (bumperDamage == false)
                            {
                                if (other.GetComponent<MultiTag>().Player1Tag == true)
                                    PlayerHealth1 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player2Tag == true)
                                    PlayerHealth2 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player3Tag == true)
                                    PlayerHealth3 -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player4Tag == true)
                                    PlayerHealth4 -= DamageTaken;
                            }
                        }
                    }
                }
            }
        }
        //Bumper damage
        //The otherPlayer below checks the ATTACKING players bumper
        if (other.gameObject.tag == "Front Bumper")
        {
            //bumper v bumper
            if (other.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude > NotMoving && timer > 1)
            {
                timer = 0;
                DamageTaken = (attackValue + other.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude) * (float)0.50f;
                //This function determins wich two players take the damage
                Bumperdamage();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        bumperDamage = false;
    }
}
