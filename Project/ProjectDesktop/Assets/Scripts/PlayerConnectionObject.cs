using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{

    //Vehicles used by Players
    public GameObject Kart;
    public GameObject Bike;
    public GameObject Ball;

    //This players Vehicle
    private GameObject myPlayerUnit;

    // Use this for initialization
    void Start()
    {

        //If the Player is not the local User
        if (!isLocalPlayer)
        {
            //Exit without running the Script
            return;
        }


        //Disable the main camera, this will allow the players camera to be activated
        //I intend to only have one camera active at a moment
        Camera.main.gameObject.SetActive(false);

        //Spawn the Kart for the player
        //In the VR version of the game this will be a bike
        //In the mobile version this will be a Ball
        CmdSpawnMyKart();

    }


    // Update is called once per frame
    void Update()
    {

        //if the user is not the local player
        if (!isLocalPlayer)
        {
            //exit without running the code
            return;
        }

        //if the User presses the button that allows the user to respawn
        if (Input.GetKey(KeyCode.R))
        {
            Respawn();
        }

    }

    public void Respawn() {
        //Destroy the Unit 
        CmdDestroyMyUnit();
        //Respawn the kart
        CmdSpawnMyKart();
    }

    //Commands - only executed on the server
    [Command]
    public void CmdSpawnMyKart()
    {
        if (connectionToClient.isReady)
        {
            //creates the object on the server
            GameObject go = Instantiate(Kart);

            myPlayerUnit = go;

            //propagate the object to all clients
            NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
        }
        else {
            StartCoroutine(WaitForReady());
        }

    }

    [Command]
    public void CmdSpawnMyBike()
    {

        //creates the object on the server
        GameObject go = Instantiate(Bike);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }

    [Command]
    public void CmdSpawnMyBall()
    {

        //creates the object on the server
        GameObject go = Instantiate(Ball);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }

    [Command]
    public void CmdDestroyMyUnit()
    {
        //Destroy the Current version of the players Unit
        Destroy(myPlayerUnit);

    }

    IEnumerator WaitForReady()
    {
        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }

        CmdSpawnMyKart();

    }

}
