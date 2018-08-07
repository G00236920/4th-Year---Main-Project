using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;


public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject Kart;
    public GameObject Bike;
    public GameObject Ball;

    GameObject myPlayerUnit;

    // Use this for initialization
    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        Camera.main.gameObject.SetActive(false);
        CmdSpawnMyBike();
    }


    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (OVRInput.Get(OVRInput.RawButton.B))
        {
            CmdDestroyMyUnit();
            CmdSpawnMyBike();
        }


    }

    //Commands - only executed on the server
    [Command]
    public void CmdSpawnMyKart()
    {

        //creates the object on the server
        GameObject go = Instantiate(Kart);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }

    [Command]
    public void CmdSpawnMyBike()
    {

        if (connectionToClient.isReady)
        {
            //creates the object on the server
            GameObject go = Instantiate(Bike);

            myPlayerUnit = go;

            //propagate the object to all clients
            NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
        }
        else
        {
            StartCoroutine(WaitForReady());
        }
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

        Destroy(myPlayerUnit);

    }

    IEnumerator WaitForReady()
    {
        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }

        CmdSpawnMyBike();

    }

}
