using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour

{
    [SerializeField] private readonly bool UseTorque = true;            // Whether or not to use torque to move the Object.
    [SerializeField] private readonly float MaxAngularVelocity = 25;    // The maximum velocity the Object can rotate at.
    [SerializeField] private readonly float JumpPower = 25;             // The force added to the ball when it jumps.

    private const float GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    public float MovePower { get; set; }
    public Rigidbody Rig { get; set; }

    private void Start()
    {

        Rig = this.GetComponentInChildren<Rigidbody>();

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
            Rig.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * MovePower);
        }
        else
        {
            // Otherwise add force in the move direction.
            Rig.AddForce(moveDirection);
        }
    }

    public void TurnBall(Vector3 ro) {
        
    }

    public void Jump() {

        // If on the ground and jump is pressed...
        if (Physics.Raycast(transform.position, -Vector3.up, GroundRayLength))
        {
            // ... add force in upwards.
            Rig.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }

    }
}

