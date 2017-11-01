using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPart : MonoBehaviour {

    public float partHealth = 1200;
    public Color lightDamage = Color.blue;
    public Color heavyDamage = Color.red;
    public float lightDamageThreshold = 800;
    public float heavyDamageThreshold = 200;
    public int minAttackSpeed = 20;
    
    [HideInInspector]
    public bool alive = true;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FrontBumper" && other.GetComponent<Damage>().carSpeed >= minAttackSpeed && alive)
        {
            float tempDamage = other.gameObject.GetComponent<Damage>().damageToTake;
            partHealth -= tempDamage;

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
