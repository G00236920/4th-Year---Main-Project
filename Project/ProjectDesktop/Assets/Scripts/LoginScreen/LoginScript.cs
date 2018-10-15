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
    private const string SERVER_IP = "52.18.149.174";
    
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
        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(SERVER_IP), PORT_NO);

        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(serverAddress);

        SendLoginDetails(client);

        bool success = getResponse(client);

        if(success){
            LoadNextScene();
        } else{
            //Show Error Asking to login again
        }

        client.Close();

    }


    void LoadNextScene(){
        //Load the Lobby scene
        SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);
    }

    void SendLoginDetails(Socket client){

        User userLogin = new User();

        userLogin.username = username;
        userLogin.password = password;

        string userToJson = JsonUtility.ToJson(userLogin);
        // Sending
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(userToJson);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(userToJson);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        client.Send(toSendLenBytes);
        client.Send(toSendBytes);
    }

    bool getResponse(Socket client){

        byte[] rcvLenBytes = new byte[4];
        client.Receive(rcvLenBytes);
        int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
        byte[] rcvBytes = new byte[rcvLen];
        client.Receive(rcvBytes);

        return BitConverter.ToBoolean(rcvBytes, 0);
    }

}