using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCam : MonoBehaviour {

    public GameObject Player;
    Vector3 Offset;

    void Start()
    {
        Offset = new Vector3(0, 2 ,-7);
    }

    void FixedUpdate()
    {
        transform.position = Player.transform.position + Offset;
    }

}
