using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    PlayerUnit player;

    void Start()
    {
        player = GetComponent<PlayerUnit>();
    }

    void Update()
    {
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

        if (Input.GetKey(KeyCode.W) && player.IsGrounded)
        {
            player.MovePower = 80;
            player.MoveVehicle(transform.forward * player.MovePower);
        }

    }

    void Akey()
    {

        if (Input.GetKey(KeyCode.A))
        {
            player.MovePower = 30;

            player.Turning = true;

            player.TurnVehicle(Vector3.down * (Time.deltaTime * 50));

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            player.MovePower = 80;
            player.Turning = false;
        }

    }

    void Skey()
    {

        if (Input.GetKey(KeyCode.S) && player.IsGrounded)
        {
            player.MovePower = 0;
        }

    }

    void Dkey()
    {
        if (Input.GetKey(KeyCode.D))
        {
            player.MovePower = 30;

            player.Turning = true;

            player.TurnVehicle(Vector3.up * (Time.deltaTime * 50));

        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            player.MovePower = 80;
            player.Turning = false;
        }

    }

    void Spacekey()
    {
        if (Input.GetKey(KeyCode.Space) && player.IsGrounded)
        {
            player.Jump();
        }
    }
}
