using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPart : MonoBehaviour {

    public float partHealth = 1200;
    public float maxHealth = 1200;
    public Color lightDamage = Color.blue;
    public Color heavyDamage = Color.red;
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
            float tempDamage = other.gameObject.GetComponent<Damage>().damageToTake;
            partHealth -= tempDamage;
            if (partHealth < 0)
                partHealth = 0;

            //float percent = partHealth/maxHealth;
            //float inversePercent = 1-percent;
            //otherPercent = (1.5f * inversePercent) + 2;
            //Debug.Log(partHealth + " percent:" + percent + " inversePercent:" + inversePercent + " otherPercent:" + otherPercent);

            if (partHealth <= heavyDamageThreshold)
            {
                //gameObject.GetComponent<Renderer>().material.color = heavyDamage;
            }
            else if (partHealth <= lightDamageThreshold)
            {
                //gameObject.GetComponent<Renderer>().material.color = lightDamage;
            }

            Debug.Log("Percentage is: "+ partHealth/maxHealth);
            gameObject.GetComponent<Renderer>().material.SetTexture("_occlusionMap", lightOcclusion);
            gameObject.GetComponent<Renderer>().material.SetFloat("_alphaCutOff", Mathf.Clamp(InverseRelationshipConvert(0, 0.8f), 0, 0.8f)); //min0 max1
            gameObject.GetComponent<Renderer>().material.SetFloat("_dmgNormal", Mathf.Clamp(InverseRelationshipConvert(1, 0.5f), 0.5f, 1)); //min1 max0.6
            gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, Mathf.Clamp(InverseRelationshipConvert(0, 100), 0, 100)); //min0 max100
        }
    }

    private float InverseRelationshipConvert(float MaxOutput, float minOutput){
       
        float percent = partHealth/maxHealth;

        Mathf.Clamp(percent, 0, 1);
        //float inversePercent = 1-percent;
        Debug.Log(((MaxOutput - minOutput ) * percent) + minOutput);
        return ((MaxOutput - minOutput ) * percent) + minOutput;
        //Debug.Log(partHealth + " percent:" + percent + " inversePercent:" + inversePercent + " otherPercent:" + otherPercent);
    }
}
