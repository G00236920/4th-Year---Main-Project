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

    const int PORT_NO1 = 5002;
    const int PORT_NO2 = 5003;
    private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");
    private List<Server> ServerList;
   

    public void ButtonClicked() {

        try
        {
            IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO1);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(serverAddress);
            
            getResponse(client);
                        
        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }

    }
    
    public void Host() {

        try
        {
            IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO2);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(serverAddress);
            
            send(client);

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }
        
    }

    void getResponse(Socket client) {

        byte[] messageBytes = new byte[2048];
        int messageInt = client.Receive(messageBytes);
        
        string messageString = Encoding.ASCII.GetString(messageBytes,0,messageInt);

        Debug.Log(messageString);

        client.Close();
    }

    void send(Socket client) {

        string str = "text";

        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(str);
        client.Send(toSendBytes);

        client.Close();
    }

}