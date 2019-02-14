using System;
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
    const int PORT_NO1 = 5004;
    const int PORT_NO2 = 5005;// not being used yet
    private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");
    //private IPAddress SERVER_IP = IPAddress.Parse("192.168.0.103");


    public void ButtonClicked()
    {
        CreateXml();// works creates xml file
        try
        {
           IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO2);

           Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           client.Connect(serverAddress);

           Debug.Log("test");

           send(client);

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }


    } //ButtonClicked 


    void send(Socket client)
    {

        string str = "text";

        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(str);

        Debug.Log(str);
        Debug.Log(toSendBytes);

        client.Send(toSendBytes);
        Debug.Log("TEST2");

        client.Close();

    }



    private static void CreateXml()
    {
        var players = new List<Player>() {
    new Player() { Username = "Ray", Score = 10 },
    new Player() { Username = "John", Score = 20 },
    new Player() { Username = "Mike", Score = 30},
    new Player() { Username = "Flan", Score = 200},
    new Player() { Username = "Kevin", Score = 40}
    };// player

        // SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);
        XDocument xdoc = new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
              // This is the root of the document
              new XElement("Scores",
              from ply in players
              select
                  new XElement("Scores", new XAttribute("UserName", ply.Username),
                  new XElement("Score", ply.Score)

                 )));

        xdoc.Save("Scores.xml"); // creates file in project/desktopProject

    }
}
public class Player
{
    public string Username { get; set; }

    public int Score { get; set; }

}
