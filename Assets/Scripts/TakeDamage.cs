using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    Rigidbody PlayerBody;
    MultiTag PlayerTag;
    BumperTag BumperTag;

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

    bool Bodydamage;

    // Use this for initialization
    void Start()
    {
        PlayerBody = GetComponentInParent<Rigidbody>();

        PlayerTag = GetComponentInParent<MultiTag>();
        BumperTag = GetComponent<BumperTag>();

        //setters for moving DMG and Stable DMG
        attackValue = AttackValue;
        sitStillDamage = SitStillAttack;

        Bodydamage = true;

        P1 = GetComponent<Player1>();
        P2 = GetComponent<Player2>();
        P3 = GetComponent<Player3>();
        P4 = GetComponent<Player4>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //PlayerBody is the Defending player
        if (PlayerBody.gameObject.tag == "Player")
        {
            //Body Damage
            //You take damage
            if (other.gameObject.GetComponentInParent<Rigidbody>() != null)
            {
                if (other.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude * (float)0.80f > PlayerBody.velocity.magnitude)
                {
                    if (PlayerBody.velocity.magnitude < NotMoving)
                    {
                        DamageTaken = (attackValue + other.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude) * sitStillDamage;
                        //This function determins who takes the damage
                        if (PlayerBody.GetComponent<MultiTag>().Player1Tag == true && Bodydamage == true)
                        {
                            P1.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                        if (PlayerBody.GetComponent<MultiTag>().Player2Tag == true && Bodydamage == true)
                        {
                            P2.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                        if (PlayerBody.GetComponent<MultiTag>().Player3Tag == true && Bodydamage == true)
                        {
                            P3.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                        if (PlayerBody.GetComponent<MultiTag>().Player4Tag == true && Bodydamage == true)
                        {
                            P4.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                    }
                    else
                    {
                        DamageTaken = (attackValue + other.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude) * (float)1.30f;
                        //This function determins who takes the damage
                        if (PlayerBody.GetComponent<MultiTag>().Player1Tag == true && Bodydamage == true)
                        {
                            P1.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                        if (PlayerBody.GetComponent<MultiTag>().Player2Tag == true && Bodydamage == true)
                        {
                            P2.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                        if (PlayerBody.GetComponent<MultiTag>().Player3Tag == true && Bodydamage == true)
                        {
                            P3.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                        if (PlayerBody.GetComponent<MultiTag>().Player4Tag == true && Bodydamage == true)
                        {
                            P4.BodyHealth -= DamageTaken;
                            Bodydamage = false;
                        }
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (PlayerBody.gameObject.tag == "Player")
            Bodydamage = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (PlayerBody.gameObject.tag == "Player")
            Bodydamage = false;
    }
}
