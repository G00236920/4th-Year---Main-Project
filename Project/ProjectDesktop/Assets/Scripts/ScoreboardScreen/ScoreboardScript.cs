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
        List<Users> users = new List<Users>() {
    new Users() { Username = "Ray", Score = 10 },
    new Users() { Username = "John", Score = 20 },
    new Users() { Username = "Mike", Score = 30},
    new Users() { Username = "Flan", Score = 200},
    new Users() { Username = "Kevin", Score = 40}
    };// Users
        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // The name of the    
            IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO1);

            // Create a TCP/IP socket.  
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.  
            client.BeginConnect(serverAddress, new AsyncCallback(ConnectCallback), client);

            Debug.Log("Connected 1");
            Debug.Log(users);
            //SendMessage(this.objectToByteArray(users));
            TestSend ts = new TestSend();
            // Release the socket.  
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Debug.Log("Connected 2");

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

    public class Users
    {
        public string Username { get; set; }

        public int Score { get; set; }

    }
public class TestSend
{

    String Host = "52.18.149.174";
    Int32 Port = 5005;

    // Use this for initialization
    void Start()
    {
        TcpClient tcpClient = new TcpClient(Host, Port);

        // Uses the GetStream public method to return the NetworkStream.
        NetworkStream netStream = tcpClient.GetStream();

        if (netStream.CanWrite)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes("Is anybody there?");
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

        if (netStream.CanRead)
        {
            // Reads NetworkStream into a byte buffer.
            byte[] bytes = new byte[tcpClient.ReceiveBufferSize];

            // Read can return anything from 0 to numBytesToRead.
            // This method blocks until at least one byte is read.
            netStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);

            // Returns the data received from the host to the console.
            string returndata = Encoding.UTF8.GetString(bytes);

            Debug.Log("This is what the host returned to you: " + returndata);

        }
        else
        {
            Debug.Log("You cannot read data from this stream.");
            tcpClient.Close();

            // Closing the tcpClient instance does not close the network stream.
            netStream.Close();
            return;
        }
        netStream.Close();
    }
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


