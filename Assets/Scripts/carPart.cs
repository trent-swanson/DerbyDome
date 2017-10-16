using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPart : MonoBehaviour {

    public float partHealth = 1200;
    public Color lightDamage = Color.blue;
    public Color heavyDamage = Color.red;
    public float lightDamageThreshold = 800;
    public float heavyDamageThreshold = 200;
    public int minAttackVelocity = 6;


    // Use this for initialization
    void Start ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Front Bumper" && other.GetComponent<Damage>().carVelo >= minAttackVelocity)
        {
            float damage = other.gameObject.GetComponent<Damage>().damageTaken;
            partHealth -= damage;

            if (partHealth <= heavyDamageThreshold)
            {
                gameObject.GetComponent<Renderer>().material.color = heavyDamage;
            }

            else if (partHealth <= lightDamageThreshold)
            {
                gameObject.GetComponent<Renderer>().material.color = lightDamage;
            }
        }
    }
}
