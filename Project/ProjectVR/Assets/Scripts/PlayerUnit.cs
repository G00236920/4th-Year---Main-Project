using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;


public class PlayerUnit : NetworkBehaviour
{
    [SerializeField] private readonly float MovePower = 10;              // The force added to the Object to move it.        
    [SerializeField] private readonly float JumpPower = .1f;             // The force added to the Object when it jumps.

    private const float groundRayLength = 1f;                           // The length of the ray to check if the Object is grounded.
    private Rigidbody rigidBody;


    public GameObject HandLeft;
    public GameObject HandRight;

    private void Start()
    {
        rigidBody = this.GetComponentInChildren<Rigidbody>();
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
            ActivateCameraForCurrentPlayer();
        }

        CheckForUserInput();
        MoveLeftHand();
        MoveRightHand();

    }

    void CheckForUserInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Move(new Vector3(0f, 0f, 0f), true);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0f, 0f, 2f), false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-2f, 0f, 1f), false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0f, 0f, -2f), false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(2f, 0f, 1f), false);
        }

    }

    public void IdentifyCurrentPlayer()
    {
        this.GetComponentInChildren<Renderer>().material.color = Color.blue;
    }

    public void ActivateCameraForCurrentPlayer()
    {
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
    }

    public void Move(Vector3 moveDirection, bool jump)
    {

        // Otherwise add force in the move direction.
        rigidBody.AddForce(moveDirection * MovePower);

        // If on the ground and jump is pressed...
        if (Physics.Raycast(transform.position, -Vector3.up, groundRayLength) && jump)
        {

            // ... add force upwards.
            rigidBody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            jump = false;
        }
    }

    public void MoveLeftHand()
    {

        HandLeft.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

        HandLeft.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

    }

    public void MoveRightHand()
    {

        HandRight.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        HandRight.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

    }
}



