using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour {

    public GameObject PlayerUnitPrefab;

    GameObject myPlayerUnit;

    // Use this for initialization
    void Start () {

        if (!isLocalPlayer) {
            return;
        }

        CmdSpawnMyUnit();
		
	}


	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {

            return;

        }

        if (Input.GetKey(KeyCode.D))
        {
            CmdMoveUnitRight();
        }

        if (Input.GetKey(KeyCode.A))
        {
            CmdMoveUnitleft();
        }

        if (Input.GetKey(KeyCode.W))
        {
            CmdMoveUnitUp();
        }

        if (Input.GetKey(KeyCode.S))
        {
            CmdMoveUnitDown();
        }

    }

    //Commands - only executed on the server
    [Command]
    void CmdSpawnMyUnit() {

        //creates the object on the server
        GameObject go = Instantiate(PlayerUnitPrefab);

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

    }

    [Command]
    void CmdMoveUnitUp()
    {
        myPlayerUnit.transform.Translate(0, 0, 1);
    }

    [Command]
    void CmdMoveUnitDown()
    {
        myPlayerUnit.transform.Translate(0, 0, -1);
    }

    [Command]
    void CmdMoveUnitleft()
    {
        myPlayerUnit.transform.Translate(-1, 0, 0);
    }

    [Command]
    void CmdMoveUnitRight()
    {
        myPlayerUnit.transform.Translate(1, 0, 0);
    }


}
