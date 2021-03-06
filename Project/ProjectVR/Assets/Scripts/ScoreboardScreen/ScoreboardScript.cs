﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardScript : MonoBehaviour
{
    //ports used for connecting to our python server
    const int PORT_NO1 = 5004;
    const int PORT_NO2 = 5005;

    private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");// ip of vm
    //private IPAddress SERVER_IP = IPAddress.Parse("192.168.0.103"); 


    public void ButtonClicked()
    {
        CreateXml();// works creates xml file

        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // The name of the    
            IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO2);// ip of vm on port 5005

            // Create a TCP/IP socket.  
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.  
            client.BeginConnect(serverAddress, new AsyncCallback(ConnectCallback), client);

            Debug.Log("Connected");

            // Release the socket.  
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }// try
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }// catch

    } //ButtonClicked 

    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.  
            Socket client = (Socket)ar.AsyncState;

            // Complete the connection.  
            client.EndConnect(ar);

            Debug.Log("Connected");

            Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

        }// try
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }// catch
    }// ConnectCallback




    private static void CreateXml()
    {
        var users = new List<Users>() {
        new Users() { Username = "Ray", Score = 10 },
        new Users() { Username = "John", Score = 20 },
        new Users() { Username = "Mike", Score = 30},
        new Users() { Username = "Flan", Score = 200},
        new Users() { Username = "Kevin", Score = 40}
        };// player

        // SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);
        XDocument xdoc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
                // This is the root of the document
                new XElement("Scores",
                from usr in users
                select
                    new XElement("Scores", new XAttribute("UserName", usr.Username),
                    new XElement("Score", usr.Score)

                    )));

        xdoc.Save("Scores.xml"); // creates file in project/desktopProject

    }// CreateXml

}// ScoreboardScript


public class Users
{
    public string Username { get; set; }

    public int Score { get; set; }

} // Users
