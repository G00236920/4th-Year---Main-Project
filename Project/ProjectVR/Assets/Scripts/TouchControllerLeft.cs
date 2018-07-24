﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TouchControllerLeft : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

    }


}