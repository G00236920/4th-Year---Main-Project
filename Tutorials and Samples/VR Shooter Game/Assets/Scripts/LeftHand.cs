using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LeftHand : MonoBehaviour
{

    private Animator Anim;
    private bool Grabbing;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        this.Grabbing = false;
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

            if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
            {

                if (this.Grabbing)
                {
                    return;
                }

                this.Grabbing = true;
                Anim.SetBool("IsGrabbingLeft", true);

            }
            if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
            {

                this.Grabbing = false;
                Anim.SetBool("IsGrabbingLeft", false);

            }

    }

}
