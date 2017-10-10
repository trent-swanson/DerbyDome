using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    Rigidbody playerBody;
    private MultiTag PlayerTag;
    private BumperTag BumperTag;

    Player1 P1;
    Player2 P2;
    Player3 P3;
    Player4 P4;

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

        P1 = GetComponent<Player1>();
        P2 = GetComponent<Player2>();
        P3 = GetComponent<Player3>();
        P4 = GetComponent<Player4>();
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
            P1.BodyHealth -= DamageTaken;
            bumperDamage = true;
            return P1.BodyHealth;
        }
        if (BumperTag.Player2bumper == true)
        {
            P2.BodyHealth -= DamageTaken;
            bumperDamage = true;
            return P2.BodyHealth;
        }
        if (BumperTag.Player3bumper == true)
        {
            P3.BodyHealth -= DamageTaken;
            bumperDamage = true;
            return P3.BodyHealth;
        }
        if (BumperTag.Player4bumper == true)
        {
            P4.BodyHealth -= DamageTaken;
            bumperDamage = true;
            return P4.BodyHealth;
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
                                    P1.BodyHealth -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player2Tag == true)
                                    P2.BodyHealth -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player3Tag == true)
                                    P3.BodyHealth -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player4Tag == true)
                                    P4.BodyHealth -= DamageTaken;
                            }
                        }
                        else
                        {
                            DamageTaken = (attackValue + playerBody.velocity.magnitude) * (float)1.30f;
                            //This function determins who takes the damage
                            if (bumperDamage == false)
                            {
                                if (other.GetComponent<MultiTag>().Player1Tag == true)
                                    P1.BodyHealth -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player2Tag == true)
                                    P2.BodyHealth -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player3Tag == true)
                                    P3.BodyHealth -= DamageTaken;
                                if (other.GetComponent<MultiTag>().Player4Tag == true)
                                    P4.BodyHealth -= DamageTaken;
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
                                if (playerBody.GetComponent<MultiTag>().Player1Tag == true)
                                    P1.BodyHealth -= DamageTaken;
                                if (playerBody.GetComponent<MultiTag>().Player2Tag == true)
                                    P2.BodyHealth -= DamageTaken;
                                if (playerBody.GetComponent<MultiTag>().Player3Tag == true)
                                    P3.BodyHealth -= DamageTaken;
                                if (playerBody.GetComponent<MultiTag>().Player4Tag == true)
                                    P4.BodyHealth -= DamageTaken;
                            }
                        }
                        else
                        {
                            DamageTaken = (attackValue + playerBody.velocity.magnitude) * (float)1.30f;
                            //This function determins who takes the damage
                            if (bumperDamage == false)
                            {
                                if (playerBody.GetComponent<MultiTag>().Player1Tag == true)
                                    P1.BodyHealth -= DamageTaken;
                                if (playerBody.GetComponent<MultiTag>().Player2Tag == true)
                                    P2.BodyHealth -= DamageTaken;
                                if (playerBody.GetComponent<MultiTag>().Player3Tag == true)
                                    P3.BodyHealth -= DamageTaken;
                                if (playerBody.GetComponent<MultiTag>().Player4Tag == true)
                                    P4.BodyHealth -= DamageTaken;
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
