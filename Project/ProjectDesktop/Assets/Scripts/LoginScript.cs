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

    [SerializeField]
    private string test;

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

            tcpclnt.Connect("127.0.0.1", 5000);
            // use the ipaddress as in the server program

            Debug.Log("Connected");

            Stream stm = tcpclnt.GetStream();

            SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);

            tcpclnt.Close();

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }

    }


}
