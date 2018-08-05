using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour
{

    public float MovePower { get; set;}
    public bool IsGrounded { get; set; }
    public Rigidbody Rig { get; set; }
    public float WheelRotation { get; set; }
    public bool Turning { get; set; }

    public float RotateZ { get; set; }

    private void Start()
    {
        Rig = this.GetComponentInChildren<Rigidbody>();
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

        WheelRotation = GetComponent<Transform>().GetChild(3).GetChild(0).gameObject.transform.localRotation.eulerAngles.y;

        RotateWheels();

    }
    
    void ActivateCameraForCurrentPlayer()
    {
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
    }

    public void MoveVehicle(Vector3 moveDirection)
    {
        Rig.AddForce(moveDirection);
    }

    public void TurnVehicle(Vector3 ro)
    {
        GetComponent<Transform>().gameObject.transform.Rotate(ro);
        GetComponent<Transform>().gameObject.transform.Rotate(ro);
    }

    void RotateWheels()
    {
        float RotateY = Rig.velocity.magnitude;

        GameObject wheel1 = GetComponent<Transform>().GetChild(3).GetChild(0).gameObject;
        GameObject wheel2 = GetComponent<Transform>().GetChild(3).GetChild(1).gameObject;
        GameObject wheel3 = GetComponent<Transform>().GetChild(3).GetChild(2).gameObject;
        GameObject wheel4 = GetComponent<Transform>().GetChild(3).GetChild(3).gameObject;

        wheel1.transform.Rotate(0, RotateY, 0);
        wheel2.transform.Rotate(0, RotateY, 0);
        wheel3.transform.Rotate(0, RotateY, 0);
        wheel4.transform.Rotate(0, RotateY, 0);

        Debug.Log(RotateZ);
        Debug.Log(WheelRotation);

    }

    public void Jump()
    {
        Rig.AddForce(new Vector3(0.0f, 20.0f, 0.0f), ForceMode.Impulse);
        IsGrounded = false;
    }

    void OnCollisionStay()
    {
        IsGrounded = true;
    }

    private void OnCollisionExit()
    {
        IsGrounded = false;
    }

}