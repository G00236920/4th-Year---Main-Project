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
<<<<<<< HEAD

    const int PORT_NO = 5002;
    private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");
    private List<Server> ServerList;
=======
    private const int PORT_NO = 5002;
    private const string SERVER_IP = "52.18.149.174";
>>>>>>> master

    public void ButtonClicked() { 

      Debug.Log("Connecting.....");

        try
        {
<<<<<<< HEAD
            Debug.Log("Connecting.....");
            IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(serverAddress);
            
=======

            Debug.Log("Connecting.....");
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(SERVER_IP), PORT_NO);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(serverAddress);

>>>>>>> master
            getResponse(client);
                        
            client.Close();

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }
    }

<<<<<<< HEAD
    void getResponse(Socket client){

        byte[] messageBytes = new byte[2048];
        int messageInt = client.Receive(messageBytes);
        
        string messageString = Encoding.ASCII.GetString(messageBytes,0,messageInt);

        Debug.Log(messageString);

    }
=======
    private void getResponse(Socket client)
    {
        byte[] bytes = new byte[2048];
        client.Receive(bytes);

        String responseData = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);

        Debug.Log(responseData);
    }

>>>>>>> master
}