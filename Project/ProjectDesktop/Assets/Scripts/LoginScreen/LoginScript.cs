using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

    //Use port 5000
    private const int port = 5000;
    //Using UDP Client
    private UdpClient Client;
    //Use the Ip and port as End Point
    private IPEndPoint Ep;

    public void ButtonClicked()
    {

        Debug.Log("Connecting.....");

        try
        {
            TcpClient tcpclnt = new TcpClient();

            tcpclnt.Connect("52.18.149.174", 5000);
            // use the ipaddress as in the server program

            Debug.Log("Connected");

            SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);

            tcpclnt.Close();

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }
    }
}