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

    private bool isGrounded;
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
            IdentifyCurrentPlayer();
        }

        CheckForUserInput();
    }

    void CheckForUserInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0f, 0f, 1f));
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-1f, 0f, 1f));
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0f, 0f, -1f));
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(1f, 0f, 1f));
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Space Pressed");
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

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Jump()
    {
        Debug.Log("Should Jump");
        this.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0.0f, 10.0f, 0.0f), ForceMode.Impulse);
        isGrounded = false;
    }

}

