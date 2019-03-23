using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
     new Users() { Username = "Ray", Score = 10}
    ,new Users() { Username = "John", Score = 20}
    ,new Users() { Username = "Mike", Score = 30}
    ,new Users() { Username = "Flan", Score = 200}
    ,new Users() { Username = "Kevin", Score = 40}
    ,new Users() { Username = "Sam", Score = 80}

    };// Users

        XDocument xdoc1 = new XDocument(
    new XDeclaration("1.0", "utf-8 ", " "),
        // This is the root of the document
        new XElement("ScoreList",
        from usr in users
        select
            new XElement("Player", new XAttribute("UserName", usr.Username),
            new XAttribute("Score",usr.Score)

            )));
       
        xdoc1.Save("ScoreList.xml"); // creates file in project/desktopProject
        string doc = xdoc1.ToString(); // converts xml to string
       
        Debug.Log(doc);

        String SERVER_IP = "52.18.149.174"; // address of server
       // String local_ip = ""; // local address for testing 
        Int32 Port = 5005; // open port on server
        //Int32 Port2 = 5006;
        Debug.Log("Connected 3");
        // Use this for initialization

        Debug.Log("Connected 4");
        TcpClient tcpClient = new TcpClient(SERVER_IP, Port);

        // Uses the GetStream public method to return the NetworkStream.
        NetworkStream netStream = tcpClient.GetStream();

        if (netStream.CanWrite)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes(doc);
            netStream.Write(sendBytes, 0, sendBytes.Length);

            StreamReader streamReader;
            NetworkStream networkStream;

            TcpListener tcpListener = new TcpListener(5555);
            tcpListener.Start();

            Debug.Log("The Server has started on port 5555");
            Debug.Log(" test 1");
            Socket serverSocket = tcpListener.AcceptSocket();
            Debug.Log(" test 1");
            try
            {
                Debug.Log("Client connected");
                networkStream = new NetworkStream(serverSocket);

                streamReader = new StreamReader(networkStream);
                var buffer = new List<byte>();

                while (serverSocket.Available > 0)
                {
                    var currByte  = new Byte[1];
                    var byteCounter = serverSocket.Receive(currByte, currByte.Length, SocketFlags.None);
                    
                    if(byteCounter.Equals(1))
                    {
                        buffer.Add(currByte[0]);
                    }
                }
                Debug.Log(buffer.ToArray());

                 serverSocket.Close();
               // Console.Read();
            }

            catch (Exception ex)
            {
               // Console.WriteLine(ex);
            }

        }// if
        else
        {
            Debug.Log("You cannot write data to this stream.");
            tcpClient.Close();

            // Closing the tcpClient instance does not close the network stream.
            netStream.Close();
            return;
        }// else


        Debug.Log("Connected 5");
        

        //Receive();
    }//SendTest

   /* 
    public  static void Receive()
    {
        Debug.Log(" In receive");
        TcpListener server = null;
        try
        {
            // Set the TcpListener on port 13000.
            Int32 port = 5006;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }

                // Shutdown and end connection
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            // Stop listening for new clients.
            server.Stop();
        }


        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
        
    }
    */
}// ScoreBoard

public class Users
{
    public string Username { get; set; }

    public int Score { get; set; }
    
}//Users




