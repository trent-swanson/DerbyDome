using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPart : MonoBehaviour {

    public float partHealth = 1200;
    public float maxHealth = 1200;
    public Color lightDamage = Color.blue;
    public Color heavyDamage = Color.red;
    public Texture2D lightNormal;
    public Texture2D lightOcclusion;

    [Space]
    public float lightDamageThreshold = 800;
    public float heavyDamageThreshold = 200;
    public int minAttackSpeed = 20;
    
    [HideInInspector]
    public bool alive = true;

    float otherPercent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FrontBumper" && other.GetComponent<Damage>().carSpeed >= minAttackSpeed && alive)
        {
            float percent = partHealth/maxHealth;
            float inversePercent = 1-percent;
            otherPercent = (1.5f * inversePercent) + 2;
            //Debug.Log(partHealth + " " + percent + " " + inversePercent + " " + otherPercent);
            
            float tempDamage = other.gameObject.GetComponent<Damage>().damageToTake;
            partHealth -= tempDamage;

            if (partHealth <= heavyDamageThreshold)
            {
                gameObject.GetComponent<Renderer>().material.color = heavyDamage;
            }
            else if (partHealth <= lightDamageThreshold)
            {
                //gameObject.GetComponent<Renderer>().material.color = lightDamage;
                gameObject.GetComponent<Renderer>().material.SetTexture("_BumpMap", lightNormal);
                gameObject.GetComponent<Renderer>().material.SetTexture("_OcclusionMap", lightOcclusion);
                gameObject.GetComponent<Renderer>().material.SetFloat("_BumpScale", otherPercent);
            }
        }
    }
}
