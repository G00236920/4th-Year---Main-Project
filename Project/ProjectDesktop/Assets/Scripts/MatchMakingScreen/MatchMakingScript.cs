using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchMakingScript : MonoBehaviour {

    
    private const int PORT_NO = 5002;
    private const string SERVER_IP = "52.18.149.174";
    private string username;
    private string password;

    public void ButtonClicked()
    {

        //Connect
        ConnectToServer();

    }

    void ConnectToServer(){
        
        Debug.Log("Connecting.....");
        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(SERVER_IP), PORT_NO);

        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(serverAddress);

        client.Close();

    }

}