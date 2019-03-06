using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;

namespace Prototype.NetworkLobby
{
	public class MatchMaker : MonoBehaviour {

		//Ports used for connecting to the Go server
		//this port is used for Hosting a game
		const int PORT_NO1 = 5002;
		//this port is used for retreiving the list of currently hosted games
		const int PORT_NO2 = 5003;
		//The Ip address of our AWS server
		private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");
		//The lobby Manager
		public LobbyManager lobbyManager;
		//The main panel shown after login
		public GameObject mainPanel;
		//The panel that will show the list of servers
		public GameObject serverPanel;

		public void findGame() {
			//connect to the Go server on AWS
			connectToServer();
			//Change to the panel that will show all hosted games
			mainPanel.SetActive(false);
			serverPanel.SetActive(true);

		}
		
		public void Host() {

			try {
				//End point using the server ip and port number
				IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO2);
				// TCP spcket type used for the connection
				Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//connect to the AWS server
				client.Connect(serverAddress);
				//send the details to AWS
				send(client);
				//start a lobby as you will be the host of the new game
				lobbyManager.StartHost();
			
			}
			catch (Exception) {
				//if the connection fails, simply used for debugging
				Debug.Log("Failed to Connect to Server");
			}
			
		}

		void getResponse(Socket client) {
			//convert the response to a readable manner for C#
			byte[] messageBytes = new byte[2048];
			//receive message from the server
			int messageInt = client.Receive(messageBytes);
			//convert the string from the byte array - should be shown in json format
			string messageString = System.Text.Encoding.ASCII.GetString(messageBytes,0,messageInt);

			try {
				//fix the json format as it does not match GO langs format
				messageString = fixJson(messageString);
				//create a list of servers - using the names of the servers amd Ip addresses
				ServerList result = JsonUtility.FromJson<ServerList>(messageString);
				//set the list so that it can be shown to the user
				ServerGUI.Instance.setList(result.list);

			}
			catch (Exception) {
				//show debug message to show that the list was not retreived, 
				//as an empty list may show the same screen
				Debug.Log("Could Not get List of Servers ");
			}
			//close the client socket
			client.Close();
		}

		void send(Socket client) {
			//Send the username over the socket
			//the ipaddress does not need to be sent as the go server can get this from the incoming connection
			byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(PlayerDetails.Instance.getUsername());
			client.Send(toSendBytes);

			client.Close();
		}

		void connectToServer(){
			try
			{
				//connect to the go server
				IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO1);
				//Tcp socket connection
				Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//connect
				client.Connect(serverAddress);
				//get response from the server
				getResponse(client);
							
			}
			catch (Exception)
			{
				//debug error to show a failure
				Debug.Log("Failed to Connect to Server");
			}
		}

		string fixJson(string value)
		{
			//used for recognising the list
			value = "{\"list\":" + value + "}";
			return value;
		}
	}
}