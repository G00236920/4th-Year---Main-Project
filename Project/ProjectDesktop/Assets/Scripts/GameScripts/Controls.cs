using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controls : NetworkBehaviour
{

    //Other scripts needed for the controls to call external methods
    PlayerUnit player;

    void Start()
    {
        //Get the Scripts needed
        player = GetComponent<PlayerUnit>();

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

    }

    void CheckForUserInput()
    {
        Wkey();     //Accelerate
        Akey();     //Turn Left
        Skey();     //Turn Right
        Dkey();     //Decelerate
        Spacekey(); //Jump
    }

    void Wkey()
    {
        //if the user pressed the Forward control, in this instance its the W key
        //Also the vehicle should be grounded, vehicles cant add force if the wheels are not on the ground
        if (Input.GetKey(KeyCode.W) && player.IsGrounded)
        {
            //Set the move power to this value
            player.MovePower = 80;
            //Move the vehicle by adding force in the forward direction
            player.MoveVehicle(transform.forward * player.MovePower);

        }

    }

    void Akey()
    {
        //if the user uses the key that turns the vehicle left
        if (Input.GetKey(KeyCode.A))
        {
            //Decrease the power of the vehicle, this is to make the turn more realistic
            player.MovePower = 30;

            //Set the boolean to true so that other methods know the vehicle is turning
            player.Turning = true;

            //Turn the vehicle using delta time, Turn left over time so its not instant
            player.TurnVehicle(Vector3.down * (Time.deltaTime * (player.MovePower*2)));

            //If the Wheels are within the turning range, this will stop the wheels from turning too much
            if (player.WheelRotation > 150 && player.WheelRotation <= 220 )
            {
                //as we are turning left, the wheel should rotate to the left
                player.RotateZ = -1;
            }
            else
            {
                //Reset the rotation, so that it doesnt continue to rotate
                player.RotateZ = 0;
            }

        }

        //If the user releases the A key
        if (Input.GetKeyUp(KeyCode.A))
        {
            //Set the vehicle power to this value
            player.MovePower = 80;
            //set the boolean to false, no longer turning
            player.Turning = false;
            //set the rotation to 0, stop the rotation
            player.RotateZ = 0;

        }

    }

    void Skey()
    {
        //If the user presses the key to stop the vehicle
        if (Input.GetKey(KeyCode.S) && player.IsGrounded)
        {
            //Set the power to 0
            player.MovePower = 0;
        }

    }

    void Dkey()
    {
        //If the user presses the key to turn right
        if (Input.GetKey(KeyCode.D))
        {
            //Set the power to a lower value, to make the turn more realistic
            player.MovePower = 30;
            //Set the turning to value, let other methods know the vehicle is turning
            player.Turning = true;
            //turn the vehicle right, by rotating the object vehicle
            player.TurnVehicle(Vector3.up * (Time.deltaTime * (player.MovePower*2)));

            //if the rotation is within the turn values, using about a 60 range, but larger number in case a thread is skipped
            if (player.WheelRotation > 150 && player.WheelRotation <= 220)
            {
                //Increase the rotation
                player.RotateZ = 1;
            }
            else
            {
                //Stop the rotation
                player.RotateZ = 0;
            }

        }

        //if the user releases the D key
        if (Input.GetKeyUp(KeyCode.D))
        {
            //Set the power back to this value
            player.MovePower = 80;
            //Set the value to false, we are no longer turning
            player.Turning = false;
            //Stop the rotation
            player.RotateZ = 0;

        }

    }

    void Spacekey()
    {
        //if the user presses the jump key
        if (Input.GetKey(KeyCode.Space) && player.IsGrounded)
        {
            //Call the jump method
            player.Jump();
        }
    }

}
