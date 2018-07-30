using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject PlayerUnitPrefab;
    public GameObject PlayerUnitPrefab2;
    public GameObject PlayerUnitPrefab3;

    GameObject myPlayerUnit;

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

    }

    //Commands - only executed on the server
    [Command]
    void CmdSpawnMyKart()
    {

        //creates the object on the server
        GameObject go = Instantiate(PlayerUnitPrefab);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }

    [Command]
    void CmdSpawnMyBike()
    {

        //creates the object on the server
        GameObject go = Instantiate(PlayerUnitPrefab2);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }

    [Command]
    void CmdSpawnMyBall()
    {

        //creates the object on the server
        GameObject go = Instantiate(PlayerUnitPrefab3);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }


}
