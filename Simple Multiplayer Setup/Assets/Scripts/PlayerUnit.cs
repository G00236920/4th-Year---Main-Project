using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer) {

            //This will prevent the command from affecting any object not owned by the client
            //as it will end the method here
            return;

        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(1, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0, 0, 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, -1);
        }

    }
}
