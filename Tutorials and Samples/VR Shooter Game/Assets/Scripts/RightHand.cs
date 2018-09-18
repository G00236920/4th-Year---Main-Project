using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightHand : MonoBehaviour {

    private Animator Anim;
    private bool Grabbing;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Grabbing = false;
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {

        transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {

            if (this.Grabbing)
            {
                return;
            }

            this.Grabbing = true;
            Anim.SetBool("IsGrabbingRight", true);

        }
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {

            this.Grabbing = false;
            Anim.SetBool("IsGrabbingRight", false);

        }

    }


}
