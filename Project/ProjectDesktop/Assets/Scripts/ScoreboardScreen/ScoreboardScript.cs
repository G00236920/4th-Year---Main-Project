using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardScript : MonoBehaviour {

    public void ButtonClicked()
    {
        SceneManager.LoadScene("2.Lobby", LoadSceneMode.Additive);
    }
	
}