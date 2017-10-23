using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFlip : MonoBehaviour
{
    RaycastHit hit;
    float Distance;
    public float timer;
    public GameObject Player;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("RayCast");
    }

    private void FlipCheck()
    {
        float CurrentRotation = Player.transform.parent.rotation.z;
        Vector3 Up = transform.TransformDirection(Vector3.up);
        if (Physics.Raycast(transform.position, Up, out hit))
        {
            timer += Time.deltaTime;
            Distance = hit.distance;
            if (hit.collider.gameObject.tag == "Ground")
            {
                if (timer >= 3)
                {
                    Player.transform.parent.parent.rotation = Quaternion.identity;
                    Player.transform.rotation = Quaternion.identity; 
                    timer = 0;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //Checks and flips the player if they are upside down
        FlipCheck();
    }
}
