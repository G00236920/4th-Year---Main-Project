using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controls : NetworkBehaviour
{

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

        if (Input.GetKey(KeyCode.W))
        {

        }

    }

    void Akey()
    {
        //if the user uses the key that turns the vehicle left
        if (Input.GetKey(KeyCode.A))
        {

        }

        //If the user releases the A key
        if (Input.GetKeyUp(KeyCode.A))
        {

        }

    }

    void Skey()
    {
        //If the user presses the key to stop the vehicle
        if (Input.GetKey(KeyCode.S))
        {

        }

    }

    void Dkey()
    {
        //If the user presses the key to turn right
        if (Input.GetKey(KeyCode.D))
        {

        }

        //if the user releases the D key
        if (Input.GetKeyUp(KeyCode.D))
        {

        }

    }

    void Spacekey()
    {
        //if the user presses the jump key
        if (Input.GetKey(KeyCode.Space))
        {

        }
    }

}
