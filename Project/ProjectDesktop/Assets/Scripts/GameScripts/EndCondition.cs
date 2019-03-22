using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        other.gameObject.transform.GetChild(0).parent = null;
        string playername = other.transform.Find("playerName").GetComponent<TextMesh>().text;
        Destroy(other.gameObject);

    }
}
