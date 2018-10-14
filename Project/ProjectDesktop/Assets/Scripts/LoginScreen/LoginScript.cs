using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System.Threading;

public class LoginScript : MonoBehaviour {

    private const int PORT_NO = 5000;
    private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");
    private TcpClient tcpclnt = new TcpClient();
    private string username;
    private string password;

    public void ButtonClicked()
    {

        //Get input from username Field
        username =  GameObject.Find ("UsernameField").GetComponent<InputField>().text;
        //Get password from password field
        password = GameObject.Find ("PasswordField").GetComponent<InputField>().text;
        //Connect
        ConnectToServer();

    }

    void ConnectToServer(){
        
        Debug.Log("Connecting.....");
        
        try
        {

            IPEndPoint remoteEP = new IPEndPoint(SERVER_IP,PORT_NO);  

            // use the ipaddress as in the server program
            tcpclnt.Connect(remoteEP);

            //Get the network Stream
            NetworkStream stream = tcpclnt.GetStream();

            new Thread(() => 
            {

               //Send the username and password to the server
                SendMessageToServer(stream);

            }).Start();

            //Listen for the server to give the result
            bool response = listenForResponse(stream);

            if(response == true){

                LoadNextScene();
            }else{
                //Dont
            }
   
            //Close the connection
            tcpclnt.Close();

        }
        catch (Exception)
        {
            Debug.Log("Failed to Connect to Server");
        }

    }
    
    void SendMessageToServer(NetworkStream stream){

        	if (stream.CanWrite) {                 

                //Create a User Object
                User userLogin = new User();

                userLogin.username = username;
                userLogin.password = password;

                string userToJson = JsonUtility.ToJson(userLogin);
                byte[] message = Encoding.UTF8.GetBytes(userToJson);

                stream.Write(message, 0 ,message.Length);

			}
    }

    void LoadNextScene(){
        //Load the Lobby scene
        SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);
    }

    bool listenForResponse(NetworkStream stream){
        
        bool response = true;

        byte[ ] buffer = new byte[tcpclnt.ReceiveBufferSize];

        int bytesRead = stream.Read(buffer, 0, tcpclnt.ReceiveBufferSize);
        stream.ReadTimeout = 1;
        string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        Debug.Log("DATA IN : "+dataReceived);

        return response;

    }

}