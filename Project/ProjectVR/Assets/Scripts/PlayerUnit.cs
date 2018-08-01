using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;


public class PlayerUnit : NetworkBehaviour
{
    [SerializeField] private readonly float MovePower = 100;              // The force added to the Object to move it.        

    private bool isGrounded;
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
            IdentifyCurrentPlayer();
        }

        CheckForUserInput();
        MoveLeftHand();
        MoveRightHand();

    }

    void CheckForUserInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Move(transform.forward * MovePower);
        }

        if (Input.GetKey(KeyCode.A))
        {

        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(-transform.forward * MovePower);
        }

        if (Input.GetKey(KeyCode.D))
        {

        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
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

    public void Move(Vector3 moveDirection)
    {
        //add force in the move direction.
        rigidBody.AddForce(moveDirection);
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

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Jump()
    {
        rigidBody.AddForce(new Vector3(0.0f, 8.0f, 0.0f), ForceMode.Impulse);
        isGrounded = false;
    }
}



