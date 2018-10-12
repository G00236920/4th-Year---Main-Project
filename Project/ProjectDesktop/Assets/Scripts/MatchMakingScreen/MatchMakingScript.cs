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
    const int PORT_NO = 5000;
    const string SERVER_IP = "52.18.149.174";

    public void ButtonClicked()
    {

        //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient();

            client.Connect("52.18.149.174", 5001);
            // use the ipaddress as in the server program

        NetworkStream stream = client.GetStream();
        
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes("Hi from Unity");

        stream.Write(bytesToSend, 0, bytesToSend.Length);

        stream.Close();
        client.Close();

    }

}
