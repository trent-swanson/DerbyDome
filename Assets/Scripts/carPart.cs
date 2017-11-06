using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPart : MonoBehaviour {

    public float partHealth = 1200;
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

    //public float tempVal = 800;

    void Update() {
        //float temp = tempVal * 0.005f;
        //Debug.Log(Mathf.Clamp(temp, 1, 3));
    }

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
                //gameObject.GetComponent<Renderer>().material.color = lightDamage;
                gameObject.GetComponent<Renderer>().material.SetTexture("_BumpMap", lightNormal);
                gameObject.GetComponent<Renderer>().material.SetTexture("_OcclusionMap", lightOcclusion);
                gameObject.GetComponent<Renderer>().material.SetFloat("_BumpScale", 2.5f);
            }
        }
    }
}
