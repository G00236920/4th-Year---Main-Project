using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour
{

    //Variables to be used to control the physics of the vehicle
    public float MovePower { get; set;}
    public bool IsGrounded { get; set; }
    public Rigidbody Rig { get; set; }
    public float WheelRotation { get; set; }
    public bool Turning { get; set; }
    public float RotateZ { get; set; }

    private void Start()
    {
        //Get the rigidbody component from the Player character
        Rig = this.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //If the unit does not belong to the current player
        if (!hasAuthority)
        {
            //Exit without running the code
            return;
        }
        if (hasAuthority)
        {
            //Activate the camera for the current
            ActivateCameraForCurrentPlayer();
        }

        //Get the current Rotation for one of the front wheels
        //This will be used to prevent the wheels for turning beyond a point that will look unrealistic
        WheelRotation = GetComponent<Transform>().GetChild(3).gameObject.transform.localRotation.eulerAngles.y;

        //Rotate the wheels of the vehicle
        //This will cover both the rotate caused by the speed of the vehicle
        //And the rotation caused by the turn of the vehicle left or right
        RotateWheels();

        //Reset the position of the wheels so that the wheels will return to the forward position
        ResetWheels();

    }
    
    void ActivateCameraForCurrentPlayer()
    {
        //activate the current users camera
        //This will prevent the user from accidentally activating another players camera
        //Having the cameras still available means we could use them later if needed
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
    }

    public void MoveVehicle(Vector3 moveDirection)
    {
        //Add force to the body of the object
        //This will send the object forward
        Rig.AddForce(moveDirection);
    }

    public void TurnVehicle(Vector3 ro)
    {
        //Rotate the vehicle to turn the vehicle
        this.GetComponent<Transform>().gameObject.transform.Rotate(0, ro.y, 0);
    }

    void RotateWheels()
    {
        //Float to hold the velocity of the vehicle
        //This will mean that the wheels can rotate based on the speed
        float RotateY = Rig.velocity.magnitude;

        //The children of the character to find each of the vehicles wheels
        GameObject wheel1 = GetComponent<Transform>().GetChild(2).gameObject;
        GameObject wheel2 = GetComponent<Transform>().GetChild(3).gameObject;
        GameObject wheel3 = GetComponent<Transform>().GetChild(4).gameObject;
        GameObject wheel4 = GetComponent<Transform>().GetChild(5).gameObject;

        //Rotate the wheels based on the velocity of the Vehicle
        //this rotation will rotate the wheels based on local position
        wheel1.transform.Rotate(0, RotateY, 0);
        wheel2.transform.Rotate(0, RotateY, 0);
        wheel3.transform.Rotate(0, RotateY, 0);
        wheel4.transform.Rotate(0, RotateY, 0);

        //Rotate the wheels based on the world space
        //Using this method prevents the wheels in the wrong direction
        wheel1.transform.Rotate(Vector3.up * RotateZ, Space.World);
        wheel2.transform.Rotate(Vector3.up * RotateZ, Space.World);

    }

    public void Jump()
    {
        //Jump the vehicle by adding for in the Y axis, using impulse force mode
        //This force mode can be changed but this method will increase the force base on the speed
        Rig.AddForce(new Vector3(0.0f, 20.0f, 0.0f), ForceMode.Impulse);
        //Reset the grounded boolean, so that the vehicle wont double jump
        IsGrounded = false;
    }

    void OnCollisionStay()
    {
        //Set grounded to true, this allows the vehicle to jump again
        IsGrounded = true;
    }

    private void OnCollisionExit()
    {
        //Once the vehicle leaves the ground or another object,
        //Set this to false to prevent a secondary jump
        IsGrounded = false;
    }

    private void ResetWheels() {

        //if the vehicle is not currently turning
        //This will be reset when the user using a control to turn
        if (!Turning)
        {
            //if the wheels are turned to the right
            if (WheelRotation < 300 && WheelRotation > 183)
            {
                RotateZ = -1;
            }
            //if the wheels are turned to the left
            else if (WheelRotation < 177 && WheelRotation > 100)
            {
                RotateZ = 1;
            }
            else
            {
                //stop the rotation 
                RotateZ = 0;
            }

        }

    }

}