//================================================================================
//carPart
//
//Purpose: Used to determine the health of the individual car parts and allows the
//textures to be changed so the player can see how much damage has been done to their car
//
//Creator: Trent Swanson
//================================================================================

using UnityEngine;

public class carPart : MonoBehaviour {

    public CarController carController;
    public bool isRoof = false;
    public float partHealth = 1200;
    public float maxHealth = 1200;
    public Texture2D lightOcclusion;

    [Space]

    public int minAttackSpeed = 20;
    
    [HideInInspector]
    public bool alive = true;

    private float otherPercent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FrontBumper" && other.GetComponent<Damage>().carSpeed >= minAttackSpeed && alive)
        {            
            float tempDamage = other.gameObject.GetComponent<Damage>().damageToTake;
            partHealth -= tempDamage;
            if (partHealth < 0)
                partHealth = 0;

            gameObject.GetComponent<Renderer>().material.SetTexture("_occlusionMap", lightOcclusion);
            gameObject.GetComponent<Renderer>().material.SetFloat("_alphaCutOff", Mathf.Clamp(InverseRelationshipConvert(0, 0.8f), 0, 0.8f)); //min0 max1
            gameObject.GetComponent<Renderer>().material.SetFloat("_dmgNormal", Mathf.Clamp(InverseRelationshipConvert(1, 0.5f), 0.5f, 1)); //min1 max0.6
            gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, Mathf.Clamp(InverseRelationshipConvert(0, 100), 0, 100)); //min0 max100
        }
        if(other.gameObject.tag == "Ground" && (carController.localVel.y >= 7 || carController.localVel.y <= -7) && alive && isRoof)
        {            
            float tempDamage = 300;
            partHealth -= tempDamage;
            if (partHealth < 0)
                partHealth = 0;

            gameObject.GetComponent<Renderer>().material.SetTexture("_occlusionMap", lightOcclusion);
            gameObject.GetComponent<Renderer>().material.SetFloat("_alphaCutOff", Mathf.Clamp(InverseRelationshipConvert(0, 0.8f), 0, 0.8f)); //min0 max1
            gameObject.GetComponent<Renderer>().material.SetFloat("_dmgNormal", Mathf.Clamp(InverseRelationshipConvert(1, 0.5f), 0.5f, 1)); //min1 max0.6
            gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, Mathf.Clamp(InverseRelationshipConvert(0, 100), 0, 100)); //min0 max100
        }
    }

    private float InverseRelationshipConvert(float MaxOutput, float minOutput)
    {
        float percent = partHealth/maxHealth;

        Mathf.Clamp(percent, 0, 1);
        return ((MaxOutput - minOutput ) * percent) + minOutput;
    }
}
