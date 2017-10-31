using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawnerR : MonoBehaviour
{
    private float timer = 0;
    private bool Active = false;

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            timer += Time.deltaTime;
            if (timer < 120)
            {
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<MeshRenderer>().enabled = true;

                Active = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Front Bumper")
        {
            Active = true;
            timer = 0;
        }
    }
}
