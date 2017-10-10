using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the bomb object.
public class BombBehaviour : MonoBehaviour
{
    // How much damage it does.
    public int damage = 49;
    // Is the bomb active? If not it cannot explode.
    public bool isActive = false;
    // How long the bomb takes to become active. Starts ticking as soon as it gets instantiated.
    // This way it can't explode as soon as a player drops it.
    public float activationTimer = 1.0f;

    // How long the explosion is delayed when a player gets near enough to the bomb. 
    public float explosionTime = 2.0f;


    
   
 

    


    void OnTriggerEnter()
    {

    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (activationTimer > 0)
        {
            activationTimer -= Time.deltaTime;
            isActive = false;
        }       
        if (activationTimer <= 0)
        {
            activationTimer = 0;
            isActive = true;
        }		
	}

    void OnTriggerEnter(Collider other)
    {
        // if others tag is "Player"
        float explosionTimer = explosionTime;
        explosionTimer -= Time.deltaTime;
        if(explosionTimer <= 0)
        {
            explosionTimer = 0;
            //other.GetComponent<CarController>();
            // damage any car in the explosion radius.
            // Destroy self.

        }
    }
}
