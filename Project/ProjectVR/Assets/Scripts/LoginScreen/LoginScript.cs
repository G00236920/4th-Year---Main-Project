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

    //Script used to allow a user to login or create a login
    public GameObject mainPanel;
    public GameObject createPanel;
    public GameObject errorPanel;
    private const int PORT_NO_LOGIN = 5000;
    private const int PORT_NO_CREATE = 5001;
    private const string SERVER_IP = "52.18.149.174";
    private string username;
    private string password;

    //Method called when the user clicks login
    public void ButtonClicked()
    {
        //Get input from username Field
        username =  GameObject.Find ("UsernameField").GetComponent<InputField>().text;
        //Get password from password field
        password = GameObject.Find ("PasswordField").GetComponent<InputField>().text;
        //Connect
        ConnectToServer();
    }

    //method called when the user creates a new account
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
            
            //did the response come back true or false,
            //true would mean that the user entered a username that has not be used yet
            bool success = getResponse(client);

            //if reponse it true
            if(success){
                //create a new user object
                User userLogin = new User();
                //set the user object name
                userLogin.username = username;
                //set the user object password
                userLogin.password = password1;
                //convert the user object to json    
                string userToJson = JsonUtility.ToJson(userLogin);
                //send the user object to the server
                //which will then create the user by adding it to a database
                Send(client, userToJson);
                //set the playerdetails singleton to the same values from the user object
                //this will carry the username for as long as the game is running
                PlayerDetails.Instance.setUsername(userLogin.username);
                //load the next scene, this will be matchmaking scene
                LoadNextScene();
            } else {
                //show a message to the user that the username is taken
                ShowError("Username is Already Taken");
            }
        }
        else{
            //show an error that the password does not meet the requirements
            ShowError("Password must be 8 or More characters, Passwords must match");
        }

    }

    //Connect to our java server
    void ConnectToServer(){
        
        //Debug.Log("Connecting.....");

        //create the client socket
        Socket client = connect(PORT_NO_LOGIN);
        //create a user object
        User userLogin = new User();
        //get the username and password entered by the user
        userLogin.username = username;
        userLogin.password = password;
        //convert the user object to json
        string userToJson = JsonUtility.ToJson(userLogin);
        //send the user object to the java server
        Send(client, userToJson);
        //get a response from the java server
        bool success = getResponse(client);
        //if the response is true
        //this means the person has entered valid login info
        if(success){
            //set the playerdetails singleton
            PlayerDetails.Instance.setUsername(userLogin.username);
            //load the match maker scene
            LoadNextScene();
        } else{
            // the user did not enter a valid login username or password
            ShowError("Invalid Login Details");
        }
        //close the client socket
        client.Close();
    }


    void LoadNextScene(){
        //Load the Lobby scene
        SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);
    }

    void Send(Socket client, String str){

        // Sending, convert to the correct formatted byte array to send to the java server
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(str);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(str);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        client.Send(toSendLenBytes);
        client.Send(toSendBytes);

    }

    bool getResponse(Socket client){
        //connevrt the incoming message to the correct values
        //this is coming from the java server to our C# client
        byte[] rcvLenBytes = new byte[4];
        client.Receive(rcvLenBytes);
        int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
        byte[] rcvBytes = new byte[rcvLen];
        client.Receive(rcvBytes);
        //return true or false
        return BitConverter.ToBoolean(rcvBytes, 0);
    }

    Socket connect(int port){
        //create a client socket and return it as this will be used more than once
        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(SERVER_IP), port);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(serverAddress);

        return client;

    }

    //change between the differnt menu panels
    void PanelSwitch(bool main, bool err, bool create){
        mainPanel.SetActive(main);
        errorPanel.SetActive(err);
        createPanel.SetActive(create);
    }

    //go back to the main panel
    public void BackButton()
    {
        PanelSwitch(true, false, false);
    }
    //show the user an error message, depending on the different error,
    //passed as a parameter
    public void ShowError(String errorMessage)
    {
        PanelSwitch(false, true, false);
        Text t = GameObject.Find("ErrorMessage").GetComponent<Text>();
        t.text = errorMessage;
    }
    
    //show the account creation panel
    public void CreatePanel()
    {        
        PanelSwitch(false, false, true);
    }

}