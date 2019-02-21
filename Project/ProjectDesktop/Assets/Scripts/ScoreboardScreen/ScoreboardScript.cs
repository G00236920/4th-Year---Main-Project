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

public class ScoreboardScript : MonoBehaviour
{
    const int PORT_NO1 = 5005;
    const int PORT_NO2 = 5006;

    private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");// ip of vm
                                                                   //private IPAddress SERVER_IP = IPAddress.Parse("127.0.0.1"); 

    public void ButtonClicked()
    {
        SendTest();
        List<Users> users = new List<Users>() {
    new Users() { Username = "Ray", Score = 10 },
    new Users() { Username = "John", Score = 20 },
    new Users() { Username = "Mike", Score = 30},
    new Users() { Username = "Flan", Score = 200},
    new Users() { Username = "Kevin", Score = 40}
    };// Users
        
    }
    public static void SendTest()
    {

        String SERVER_IP = "52.18.149.174";
        Int32 Port = 5005;

        Debug.Log("Connected 3");
        // Use this for initialization

        Debug.Log("Connected 4");
        TcpClient tcpClient = new TcpClient(SERVER_IP, Port);

        // Uses the GetStream public method to return the NetworkStream.
        NetworkStream netStream = tcpClient.GetStream();

        if (netStream.CanWrite)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes("Ahoy there Kevin");
            netStream.Write(sendBytes, 0, sendBytes.Length);
        }
        else
        {
            Debug.Log("You cannot write data to this stream.");
            tcpClient.Close();

            // Closing the tcpClient instance does not close the network stream.
            netStream.Close();
            return;
        }



    }//here
}

public class Users
{
    public string Username { get; set; }

    public int Score { get; set; }

}


/*var users = new List<Users>() {
new Users() { Username = "Ray", Score = 10 },
new Users() { Username = "John", Score = 20 },
new Users() { Username = "Mike", Score = 30},
new Users() { Username = "Flan", Score = 200},
new Users() { Username = "Kevin", Score = 40}
};// player
/*
private static string SendScores()
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

//string xml = xdoc.ToString();

//xdoc.Save("Scores.xml"); // creates file in project/desktopProject
return ("0");
}// SendScores

}// ScoreboardScript


public class Users
{
public string Username { get; set; }

public int Score { get; set; }

} // Users*/


