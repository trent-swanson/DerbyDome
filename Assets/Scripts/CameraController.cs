using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CameraController : MonoBehaviour
{
    [Tooltip("Automatically matches, no need to drag and drop.")]
    public XboxController controller;

    // Camera moves around this.
    [Tooltip("Automatically matches, no need to drag and drop. You still can if you want to and it will work if you dragged the correct object in. If you drag the incorrect object in it will correct itself.")]
    public GameObject pivot = null;
    [Tooltip("How far back from the car the camera is.")]
    public float dist = 0.0f;
    [Tooltip("How high above the car the camera is.")]
    public float height = 0.0f;

    void CheckPivot(string a_name)
    {
        // Check if pivot is unassigned.
        if (pivot == null)
        {
            AssignPivot();
        }
        else if (pivot.name != a_name)
        {
            AssignPivot();
        }
    }
    void AssignPivot() { }



    // Use this for initialization
    void Start ()
    {
        CheckPivot("Pivot1");


            // Check which player the camera belongs to.
            if ((gameObject.tag == "Player1Cam") || (gameObject.name == "Player1Cam"))
            {
                // Assign the pivot.
                pivot = GameObject.Find("Pivot1");
                // Set the default camera position.
                pivot.transform.position += Vector3.up * height;
                // Set the default camera rotation.
                transform.LookAt(pivot.transform);
                // Assign the controller.
                controller = XboxController.First;
            }
            if ((gameObject.tag == "Player2Cam") || (gameObject.name == "Player2Cam"))
            {
                pivot = GameObject.Find("Pivot2");
                pivot.transform.position += Vector3.up * height;
                transform.LookAt(pivot.transform);
                controller = XboxController.Second;
            }
            if ((gameObject.tag == "Player3Cam") || (gameObject.name == "Player3Cam"))
            {
                pivot = GameObject.Find("Pivot3");
                pivot.transform.position += Vector3.up * height;
                transform.LookAt(pivot.transform);
                controller = XboxController.Third;
            }
            if ((gameObject.tag == "Player4Cam") || (gameObject.name == "Player4Cam"))
            {
                pivot = GameObject.Find("Pivot4");
                pivot.transform.position += Vector3.up * height;
                transform.LookAt(pivot.transform);
                controller = XboxController.Fourth;
            }
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.LookAt(pivot.transform);

        transform.position = pivot.transform.position + (dist * -transform.forward);
		
	}
}
