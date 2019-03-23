using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EndCondition : NetworkBehaviour
{

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.Find("Camera")){
            other.transform.Find("Camera").parent = null;
        }
        
        NetworkManager.singleton.client.connection.playerControllers[0].gameObject.GetComponent<PlayerConnectionObject>().CmdDestroyUnit(other.gameObject);
        
    }

}
