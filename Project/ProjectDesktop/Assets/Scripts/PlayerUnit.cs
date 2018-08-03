using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour
{
    private readonly float MovePower = 100;              // The force added to the Object to move it.    
    private bool isGrounded;
    private Rigidbody rigidBody;
    private float WheelRotation;
    private bool turning;

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
        }

        //Check the Current Rotation of the Vechiles front wheels
        WheelRotation = GetComponent<Transform>().GetChild(3).GetChild(0).gameObject.transform.localRotation.eulerAngles.y;

        //Check if the user is input Controls
        CheckForUserInput();

    }

    void CheckForUserInput()
    {

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            ResetWheelPosition();
            MoveVehicle(transform.forward * MovePower);
        }
        if (Input.GetKey(KeyCode.A))
        {
            turning = true;
            TurnWheelsLeft();
            TurnVehicle(Vector3.down * (Time.deltaTime * 40));
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            turning = false;
        }
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            MoveVehicle(-transform.forward * MovePower);
        }
        if (Input.GetKey(KeyCode.D))
        {
            turning = true;
            TurnWheelsRight();
            TurnVehicle(Vector3.up * (Time.deltaTime * 40));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            turning = false;
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

    }

    public void ActivateCameraForCurrentPlayer()
    {
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
    }

    public void MoveVehicle(Vector3 moveDirection)
    {
        //Add force in the move direction.
        rigidBody.AddForce(moveDirection);
    }

    void TurnWheels(Vector3 ro)
    {
        GetComponent<Transform>().GetChild(3).GetChild(0).gameObject.transform.Rotate(ro);
        GetComponent<Transform>().GetChild(3).GetChild(1).gameObject.transform.Rotate(ro);
    }

    void TurnVehicle(Vector3 ro)
    {
        GetComponent<Transform>().gameObject.transform.Rotate(ro);
        GetComponent<Transform>().gameObject.transform.Rotate(ro);
    }

    void RotateWheels(Vector3 ro)
    {
        GetComponent<Transform>().GetChild(3).GetChild(0).gameObject.transform.Rotate(ro);
        GetComponent<Transform>().GetChild(3).GetChild(1).gameObject.transform.Rotate(ro);
        GetComponent<Transform>().GetChild(3).GetChild(2).gameObject.transform.Rotate(ro);
        GetComponent<Transform>().GetChild(3).GetChild(3).gameObject.transform.Rotate(ro);
    }

    void Jump()
    {
        rigidBody.AddForce(new Vector3(0.0f, 20.0f, 0.0f), ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    void ResetWheelPosition()
    {

        if (WheelRotation >= 329 && WheelRotation < 360 && !turning)
        {
            TurnWheels(Vector3.forward * (Time.deltaTime * 50));
        }

        if (WheelRotation < 31 && WheelRotation > 0 && !turning)
        {
            TurnWheels(Vector3.back * (Time.deltaTime * 50));
        }

    }

    void TurnWheelsRight()
    {

        if (WheelRotation < 30 || WheelRotation >= 329)
        {
            TurnWheels(Vector3.forward * (Time.deltaTime * 50));
        }

    }

    void TurnWheelsLeft()
    {

        if (WheelRotation > 330 || WheelRotation <= 31)
        {
            TurnWheels(Vector3.back * (Time.deltaTime * 50));
        }

    }

}