using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour
{
    [SerializeField] private readonly float MovePower = 10;              // The force added to the Object to move it.
    [SerializeField] private readonly bool UseTorque = true;            // Whether or not to use torque to move the Object.
    [SerializeField] private readonly float MaxAngularVelocity = 25;      // The maximum velocity the Object can rotate at.

    private Rigidbody rigidBody;

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
        }

        CheckForUserInput();

    }

    void CheckForUserInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0f, 0f, 1f), false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-1f, 0f, 1f), false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0f, 0f, -1f), false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(1f, 0f, 1f), false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
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
            this.GetComponentInChildren<Rigidbody>().AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * MovePower);
        }
        else
        {
            // Otherwise add force in the move direction.
            this.GetComponentInChildren<Rigidbody>().AddForce(moveDirection * MovePower);
        }

    }
}

