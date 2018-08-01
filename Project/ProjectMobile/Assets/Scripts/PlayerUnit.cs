using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour
{
    [SerializeField] private readonly float MovePower = 10;             // The force added to the Object to move it.
    [SerializeField] private readonly bool UseTorque = true;            // Whether or not to use torque to move the Object.
    [SerializeField] private readonly float MaxAngularVelocity = 25;    // The maximum velocity the Object can rotate at.
    [SerializeField] private readonly float JumpPower = 25;             // The force added to the ball when it jumps.

    private const float GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody rigidBody;
    private bool MidJump = false;

    private void Start()
    {
        rigidBody = this.GetComponentInChildren<Rigidbody>();
        // Set the maximum angular velocity.
        this.GetComponentInChildren<Rigidbody>().maxAngularVelocity = MaxAngularVelocity;
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
    }

    void CheckForUserInput()
    {
        if (rigidBody.velocity.y == 0)
        {
            MidJump = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Move(transform.forward * MovePower, false);
        }

        if (Input.GetKey(KeyCode.A))
        {

        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(-transform.forward * MovePower, false);
        }

        if (Input.GetKey(KeyCode.D))
        {

        }

        if (Input.GetKeyDown(KeyCode.Space) && !MidJump)
        {
            MidJump = true;
            Move(new Vector3(0f, 0f, 0f), true);
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

        // If using torque to rotate the ball...
        if (UseTorque)
        {
            // ... add torque around the axis defined by the move direction.
            rigidBody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * MovePower);
        }
        else
        {
            // Otherwise add force in the move direction.
            rigidBody.AddForce(moveDirection);
        }
        // If on the ground and jump is pressed...
        if (Physics.Raycast(transform.position, -Vector3.up, GroundRayLength) && jump)
        {
            // ... add force in upwards.
            rigidBody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);

        }

    }

}

