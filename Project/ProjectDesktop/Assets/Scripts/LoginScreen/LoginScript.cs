using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class LoginScript : MonoBehaviour {

    //Using UDP Client
    private UdpClient Client;
    //Use the Ip and port as End Point
    private IPEndPoint Ep;

    public void ButtonClicked()
    {

        Debug.Log("Connecting.....");

        try
        {
            TcpClient tcpclnt = new TcpClient();

            // use the ipaddress as in the server program
            tcpclnt.Connect("52.18.149.174", 5000);
              
            //Get the network Stream
            NetworkStream stream = tcpclnt.GetStream();

			if (stream.CanWrite) {                 
		
                //Get input from username Field
                String username =  GameObject.Find ("UsernameField").GetComponent<InputField>().text;
                //Get password from password field
                String password = GameObject.Find ("PasswordField").GetComponent<InputField>().text;

                //Create a User Object
                User userLogin = new User(username, password);

                BinaryFormatter bf = new BinaryFormatter();
                
                bf.Serialize(stream, userLogin);

                //Send the message      
				//stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
 
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