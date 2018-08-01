using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour
{
    [SerializeField] private readonly float MovePower = 100;              // The force added to the Object to move it.        

    private bool isGrounded;
    private Rigidbody rigidBody;
    private float WheelRotation;

    private void Start()
    {
        rigidBody = this.GetComponentInChildren<Rigidbody>();
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

            WheelRotation = GetComponent<Transform>().GetChild(3).GetChild(0).gameObject.transform.localRotation.eulerAngles.y;
        }

        CheckForUserInput();
    }

    void CheckForUserInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Move(transform.forward * MovePower);
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (WheelRotation > 330 || WheelRotation <= 31)
            {
                TurnWheels(Vector3.back * (Time.deltaTime * 50));
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(-transform.forward * MovePower);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (WheelRotation < 30 || WheelRotation >= 329)
            {
                TurnWheels(Vector3.forward * (Time.deltaTime * 50));
            }
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
        // add force in the move direction.
        rigidBody.AddForce(moveDirection);
    }

    void OnCollisionStay()
    {
       isGrounded = true;
    }

    void Jump()
    {
        rigidBody.AddForce(new Vector3(0.0f, 4.0f, 0.0f), ForceMode.Impulse);
        isGrounded = false;
    }

    void TurnWheels(Vector3 ro) {
            GetComponent<Transform>().GetChild(3).GetChild(0).gameObject.transform.Rotate(ro);
            GetComponent<Transform>().GetChild(3).GetChild(1).gameObject.transform.Rotate(ro);
    }

}



