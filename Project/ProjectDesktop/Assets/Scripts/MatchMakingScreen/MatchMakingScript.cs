using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchMakingScript : MonoBehaviour {
    const int PORT_NO = 5001;
    const string SERVER_IP = "127.0.0.1";
    private Socket _clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

    public void ButtonClicked(){

        try
        {
            _clientSocket.Connect(SERVER_IP,PORT_NO);

            byte[] data = Encoding.ASCII.GetBytes("RequestList");

            _clientSocket.Send(data);

        }
        catch(SocketException ex)
        {
            Debug.Log(ex.Message);
        }

    }

}