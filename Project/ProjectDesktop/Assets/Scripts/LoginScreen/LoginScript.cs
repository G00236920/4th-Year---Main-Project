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

            tcpclnt.Connect("52.18.149.174", 5000);
            // use the ipaddress as in the server program
              
            NetworkStream stream = tcpclnt.GetStream();

			if (stream.CanWrite) {                 
		
                String username =  GameObject.Find ("UsernameField").GetComponent<InputField>().text;
                String password = GameObject.Find ("PasswordField").GetComponent<InputField>().text;

                String data = username + " " + password;
				             
				byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(data); 				
				      
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
 
			}          

            SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);

            tcpclnt.Close();

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }
    }

}