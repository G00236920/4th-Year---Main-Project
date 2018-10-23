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
    public GameObject MainPanel;
    public GameObject ServerPanel;
    private const int PORT_NO = 5002;
    private const string SERVER_IP = "52.18.149.174";

    public void ButtonClicked() { 

      Debug.Log("Connecting.....");

        try
        {

            Debug.Log("Connecting.....");
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(SERVER_IP), PORT_NO);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(serverAddress);

            getResponse(client);
                        
            client.Close();

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }
    }

    private void getResponse(Socket client)
    {
        byte[] bytes = new byte[2048];
        client.Receive(bytes);

        String responseData = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);

        Debug.Log(responseData);

        ServerPanel.gameObject.SetActive (true);
        MainPanel.gameObject.SetActive (false);

    }

}