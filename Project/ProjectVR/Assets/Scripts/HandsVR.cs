using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandsVR : MonoBehaviour {

    GameObject LeftHand;
    GameObject RightHand;
    Vector3 HandleBarsPositionL;
    Vector3 HandleBarsPositionR;
    Quaternion HandleBarsRotationL;
    Quaternion HandleBarsRotationR;

    // Use this for initialization
    void Start () {

        LeftHand = GetComponent<Transform>().GetChild(1).gameObject;
        RightHand = GetComponent<Transform>().GetChild(2).gameObject;

    }

    // Update is called once per frame
    void Update()
    {


    }

    void GrabHandleBars() {

        HandleBarsPositionL = GetComponent<Transform>().GetChild(5).GetChild(0).localPosition;
        HandleBarsPositionR = GetComponent<Transform>().GetChild(5).GetChild(1).localPosition;
        HandleBarsRotationL = GetComponent<Transform>().GetChild(5).GetChild(0).localRotation;
        HandleBarsRotationR = GetComponent<Transform>().GetChild(5).GetChild(1).localRotation;

        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            LeftHand.transform.localPosition = new Vector3(HandleBarsPositionL.x, HandleBarsPositionL.y, HandleBarsPositionL.z);
            LeftHand.transform.localRotation = new Quaternion(HandleBarsRotationL.x, HandleBarsRotationL.y, HandleBarsRotationL.z, HandleBarsRotationL.w);
        }


        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            RightHand.transform.localPosition = new Vector3(HandleBarsPositionR.x, HandleBarsPositionR.y, HandleBarsPositionR.z);
            RightHand.transform.localRotation = new Quaternion(HandleBarsRotationR.x, HandleBarsRotationR.y, HandleBarsRotationR.z, HandleBarsRotationR.w);
        }

    }

}
