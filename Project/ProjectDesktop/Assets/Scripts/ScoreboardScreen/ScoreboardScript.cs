using System;
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

    }

    public static void SendTest()
    {
        List<Users> users = new List<Users>() {
    new Users() { Username = "Ray", Score = 10 },
    new Users() { Username = "John", Score = 20 },
    new Users() { Username = "Mike", Score = 30},
    new Users() { Username = "Flan", Score = 200},
    new Users() { Username = "Kevin", Score = 40},
    new Users() { Username = "Sam", Score = 80}

    };// Users

       // for (int i = 0; i < users.Count; i++)
        //{
        //    Debug.Log(users[i]);
        //}
        XDocument xdoc = new XDocument(
    new XDeclaration("1.0", "utf-8", "yes"),
        // This is the root of the document
        new XElement("ScoreList",
        from usr in users
        select
            new XElement("Player", new XAttribute("UserName", usr.Username),
            new XAttribute("Score", usr.Score)

            )));
        xdoc.Save("ScoreList.xml"); // creates file in project/desktopProject
        String doc = xdoc.ToString();
        Debug.Log(doc);
        String SERVER_IP = "52.18.149.174";
        Int32 Port = 5005;


        // Use this for initialization


        TcpClient tcpClient = new TcpClient(SERVER_IP, Port);
        Debug.Log("Connected 1");
        // Uses the GetStream public method to return the NetworkStream.
        NetworkStream netStream = tcpClient.GetStream();
        Debug.Log("Connected 2");
        if (netStream.CanWrite)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes(doc);
            netStream.Write(sendBytes, 0, sendBytes.Length);
            Debug.Log("sent");



            // StreamWriter sw = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);
            StreamReader sr = new StreamReader(tcpClient.GetStream(), Encoding.ASCII);
            string received = sr.ReadToEnd();
            XmlDocument xm = new XmlDocument();
            xm.LoadXml(received);
            xm.Save("newList.xml");
            Debug.Log(received);


        }
        else
        {
            Debug.Log("You cannot write data to this stream.");
            tcpClient.Close();
            Debug.Log("tcp closed");
            // Closing the tcpClient instance does not close the network stream.
            netStream.Close();
            Debug.Log("netstream closed");
            return;
        }

        Debug.Log("connection closed");

    }//SentTest


}// ScoreBoard

public class Users
{
    public string Username { get; set; }

    public int Score { get; set; }

    public int Rank { get; set; }

}//Users




