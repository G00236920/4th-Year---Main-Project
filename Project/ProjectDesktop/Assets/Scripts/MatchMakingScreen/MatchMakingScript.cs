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

    const int PORT_NO = 5001;
    private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");

    public void ButtonClicked() { 

      Debug.Log("Connecting.....");

        try
        {
            TcpClient tcpclnt = new TcpClient();

            // use the ipaddress as in the server program
            tcpclnt.Connect(SERVER_IP, PORT_NO);
              
            //Get the network Stream
            NetworkStream stream = tcpclnt.GetStream();

			if (stream.CanWrite) {                 

                //Create a User Object
                Server server = new Server(IPAddress.Parse("52.18.149.174"), "me");

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, server);
 
			}          
            
            //Load the Lobby scene
            SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);

                        
            tcpclnt.Close();

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }
    }
}