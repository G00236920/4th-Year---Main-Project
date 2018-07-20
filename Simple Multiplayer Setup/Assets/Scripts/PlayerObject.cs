using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

    public GameObject PlayerUnitPrefab;

    // Use this for initialization
    void Start () {

        if (!isLocalPlayer) {

            return;
        }
		
	}


	// Update is called once per frame
	void Update () {
		
	}

    //Commands - only executed on the server
    [Command]
    void CmdSpawnMyUnit() {

        //creates the object on the server
        GameObject go = Instantiate(PlayerUnitPrefab);

        //propagate the object to all clients
        NetworkServer.Spawn(go);

    }

}
