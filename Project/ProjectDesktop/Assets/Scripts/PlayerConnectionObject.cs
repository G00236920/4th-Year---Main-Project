using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject Kart;
    public GameObject Bike;
    public GameObject Ball;

    private GameObject myPlayerUnit;

    // Use this for initialization
    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        Camera.main.gameObject.SetActive(false);
        CmdSpawnMyKart();
    }


    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKey(KeyCode.R))
        {
            CmdDestroyMyUnit();
            CmdSpawnMyKart();
        }

    }

    //Commands - only executed on the server
    [Command]
    void CmdSpawnMyKart()
    {

        //creates the object on the server
        GameObject go = Instantiate(Kart);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }

    [Command]
    void CmdSpawnMyBike()
    {

        //creates the object on the server
        GameObject go = Instantiate(Bike);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }

    [Command]
    void CmdSpawnMyBall()
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

        Destroy(myPlayerUnit);

    }

}
