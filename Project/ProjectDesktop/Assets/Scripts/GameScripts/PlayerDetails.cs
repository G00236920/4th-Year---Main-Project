using UnityEngine;
using System.Collections;

public class PlayerDetails : MonoBehaviour {

    /*
    Used to store player details, Mainly the username and starting positions 
    but may be needed later for other purposes
    Will be used as a singleton
     */
    private static PlayerDetails _instance;
    private static string username;
    private static int startPosition;

    //The only instance of the class object
    public static PlayerDetails Instance { get { return _instance; } }

    private void Awake () {
        //if there is already an instance of this object, destroy the new one
        if (_instance != null && _instance != this) {
            Destroy (this.gameObject);
            return;
        }
        //set this object to be the only instance of player Details
        _instance = this;
        //Dont destroy this object between scenes
        DontDestroyOnLoad (this.gameObject);
    }

    //Getters and setters  
    public void setUsername (string u) {
        username = u;
    }

    public string getUsername () {
        return username;
    }

    public void setPos (int pos) {
        startPosition = pos;
    }

    public int getPos () {
        return startPosition;
    }
}