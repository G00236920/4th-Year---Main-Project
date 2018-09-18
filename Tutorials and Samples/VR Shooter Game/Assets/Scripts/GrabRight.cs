using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRight : MonoBehaviour {

    public LayerMask grabMask;
    public float grabRadius;
    public OVRInput.Controller controller;

    private GameObject grabbedObject;
    private bool grabbing;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) && !grabbing)
        {
            GrabObject();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) && grabbing)
        {
            DropObject();
        }

    }

    void GrabObject() {

        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0) {

            int closetHit = 0;

            for (int i = 0; i <hits.Length; i++) 
            {

                if (hits[i].distance > hits[closetHit].distance) {

                    closetHit = i;

                }

            }

            grabbedObject = hits[closetHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

            grabbedObject.transform.parent = transform;
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.rotation = new Quaternion(0,0,0,0);


        }

    }

    void DropObject()
    {
        grabbing = false;

        if (grabbedObject != null) {

            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller) ;

            grabbedObject = null;

        }

    }

}
