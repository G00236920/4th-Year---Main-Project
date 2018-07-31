using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCam : MonoBehaviour {

    public GameObject Player;
    Vector3 _offset;

    void Start()
    {
        _offset = new Vector3(0, 1 ,-5);
    }

    void FixedUpdate()
    {
        transform.position = Player.transform.position + _offset;
    }

}
