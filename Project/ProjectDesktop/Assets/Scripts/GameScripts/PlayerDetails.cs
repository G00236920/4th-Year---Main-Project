using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour {

    private static PlayerDetails _instance;
    private static string username;

    public static PlayerDetails Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        } 
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void setUsername(string u){
        username = u;
    }

    public string getUsername(){
        return username;
    }
}
