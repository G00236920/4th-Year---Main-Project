using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour
{
    [SerializeField] private readonly float MovePower = 10;              // The force added to the Object to move it.        

    private Rigidbody rigidBody;

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

        CheckForUserInput();

    }

    void CheckForUserInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0f, 0f, 2f), false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-2f, 0f, 1f), false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0f, 0f, -2f), false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(2f, 0f, 1f), false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
           
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

        // add force in the move direction.
        rigidBody.AddForce(moveDirection * MovePower);

    }
}



