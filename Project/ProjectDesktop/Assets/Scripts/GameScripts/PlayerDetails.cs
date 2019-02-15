using UnityEngine;
using System.Collections;

public class PlayerDetails : MonoBehaviour {

    private static PlayerDetails _instance;
    private static string username;
    private static int startPosition;

    public static PlayerDetails Instance { get { return _instance; } }

    private void Awake () {
        if (_instance != null && _instance != this) {
            Destroy (this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad (this.gameObject);
    }

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