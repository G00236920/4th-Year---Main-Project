using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;

public class Controls : NetworkBehaviour
{

    //Other scripts needed for the controls to call external methods
    PlayerUnit player;
    GameObject LeftHand;
    GameObject RightHand;

    void Start()
    {
        //Get the Scripts needed
        player = GetComponent<PlayerUnit>();
        LeftHand = GetComponent<Transform>().GetChild(1).gameObject;
        RightHand = GetComponent<Transform>().GetChild(2).gameObject;

    }

    void Update()
    {

        //If the current user does not own the player prefab
        if (!hasAuthority)
        {
            //Return without running the code
            return;
        }

        //check if the user has pressed any controls
        CheckForUserInput();

        MoveLeftHand();
        MoveRightHand();

    }

    void CheckForUserInput()
    {
        Accelerate();    //Accelerate
        Left();          //Turn Left
        Stop();          //Turn Right
        Right();         //Decelerate
        Jumpkey();       //Jump
    }

    void Accelerate()
    {
        //if the user pressed the Forward control, in this instance its the W key
        //Also the vehicle should be grounded, vehicles cant add force if the wheels are not on the ground
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            //Set the move power to this value
            player.MovePower = 60;
            //Move the vehicle by adding force in the forward direction
            player.MoveVehicle(transform.forward * player.MovePower);

        }

    }

    void Left()
    {
        //if the user uses the key that turns the vehicle left
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft))
        {
            //Decrease the power of the vehicle, this is to make the turn more realistic
            player.MovePower = 30;

            //Set the boolean to true so that other methods know the vehicle is turning
            player.Turning = true;

            //Turn the vehicle using delta time, Turn left over time so its not instant
            player.TurnVehicle(Vector3.down * (Time.deltaTime * 50));

        }

        //If the user releases the A key
        if (OVRInput.GetUp(OVRInput.RawButton.LThumbstickLeft))
        {
            //Set the vehicle power to this value
            player.MovePower = 60;
            //set the boolean to false, no longer turning
            player.Turning = false;

        }

    }

    void Stop()
    {

        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) && OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {

            player.MovePower = 0;

        }

    }

    void Right()
    {
        //If the user presses the key to turn right
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight))
        {
            //Set the power to a lower value, to make the turn more realistic
            player.MovePower = 30;
            //Set the turning to value, let other methods know the vehicle is turning
            player.Turning = true;
            //turn the vehicle right, by rotating the object vehicle
            player.TurnVehicle(Vector3.up * (Time.deltaTime * 50));

        }

        //if the user releases the D key
        if (OVRInput.GetUp(OVRInput.RawButton.LThumbstickRight))
        {
            //Set the power back to this value
            player.MovePower = 60;
            //Set the value to false, we are no longer turning
            player.Turning = false;

        }

    }

    void Jumpkey()
    {

        if (OVRInput.Get(OVRInput.RawButton.A) && player.IsGrounded)
        {

            //Call the jump method
            player.Jump();

        }

    }

    public void MoveLeftHand()
    {
        LeftHand.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        LeftHand.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
    }

    public void MoveRightHand()
    {
        RightHand.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        RightHand.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
    }

}
