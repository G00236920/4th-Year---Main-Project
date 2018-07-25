using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;

public class PlayerUnit : NetworkBehaviour
{
    public GameObject HandLeft;
    public GameObject HandRight;

    // Use this for initialization
    void Start()
    {
        InputTracking.Recenter();
    }

    // Update is called once per frame
    void Update()
    {

        if (!hasAuthority)
        {
            return;
        }
        if (hasAuthority)
        {
            //Make Camera Active for Current Player Only
            GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);

        }

        this.GetComponentInChildren<Renderer>().material.color = Color.blue;

        MoveLeftHand();
        MoveRightHand();


        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0, 0, 1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(1, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, -1);
        }

    }

    public void MoveLeftHand() {
        HandLeft.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        HandLeft.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
    }

    public void MoveRightHand() {
        HandRight.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        HandRight.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
    }

}

