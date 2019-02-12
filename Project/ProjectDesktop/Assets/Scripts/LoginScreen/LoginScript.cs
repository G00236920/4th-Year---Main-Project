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

    public GameObject mainPanel;
    public GameObject createPanel;
    public GameObject errorPanel;
    private const int PORT_NO_LOGIN = 5000;
    private const int PORT_NO_CREATE = 5001;
    private const string SERVER_IP = "52.18.149.174";
    private string username;
    private string password;

    public void Connect()
    {
        //Get input from username Field
        username =  GameObject.Find ("UsernameField").GetComponent<InputField>().text;
        //Get password from password field
        password = GameObject.Find ("PasswordField").GetComponent<InputField>().text;
        //Connect
        ConnectToServer();
    }

    public void CreateUser(){

        Socket client = connect(PORT_NO_CREATE);

        //Get input from username Field
        String username =  GameObject.Find ("Username2").GetComponent<InputField>().text;

        Send(client, username);

        //Get password from password field
        String password1 = GameObject.Find ("Password1").GetComponent<InputField>().text;
        //Get password from password field to be used to verify
        String password2 = GameObject.Find ("Password2").GetComponent<InputField>().text;

        if(password1.Equals(password2) && password1.Length >=8){

            bool success = getResponse(client);

            if(success){
                
                User userLogin = new User();

                userLogin.username = username;
                userLogin.password = password1;

                string userToJson = JsonUtility.ToJson(userLogin);

                Send(client, userToJson);
                
                LoadNextScene();
            } else {
                ShowError("Username is Already Taken");
            }
        }
        else{
            ShowError("Password must be 8 or More characters, Passwords must match");
        }

    }

    void ConnectToServer(){
        
        Debug.Log("Connecting.....");

        Socket client = connect(PORT_NO_LOGIN);

        User userLogin = new User();

        userLogin.username = username;
        userLogin.password = password;

        string userToJson = JsonUtility.ToJson(userLogin);

        Send(client, userToJson);

        bool success = getResponse(client);

        if(success){
            LoadNextScene();
        } else{
            ShowError("Invalid Login Details");
        }

        client.Close();
    }


    void LoadNextScene(){
        //Load the Lobby scene
        SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);
    }

    void Send(Socket client, String str){

        // Sending
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(str);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(str);
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

    Socket connect(int port){

        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(SERVER_IP), port);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(serverAddress);

        return client;

    }

    void PanelSwitch(bool main, bool err, bool create){
        mainPanel.SetActive(main);
        errorPanel.SetActive(err);
        createPanel.SetActive(create);
    }

    public void BackButton()
    {
        PanelSwitch(true, false, false);
    }

    public void ShowError(String errorMessage)
    {
        PanelSwitch(false, true, false);
        Text t = GameObject.Find("ErrorMessage").GetComponent<Text>();
        t.text = errorMessage;
    }
    
    public void CreatePanel()
    {        
        PanelSwitch(false, false, true);
    }

}