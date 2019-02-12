using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Prototype.NetworkLobby
{
	public class MatchMaker : MonoBehaviour {

		const int PORT_NO1 = 5002;
		const int PORT_NO2 = 5003;
		private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");
		private List<Server> ServerList;

		public LobbyManager lobbyManager;

		public GameObject mainPanel;
		public GameObject serverPanel;

		public void findGame() {

			connectToServer();
			
			mainPanel.SetActive(false);
			serverPanel.SetActive(true);

		}
		
		public void Host() {

			try
			{
				IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO2);

				Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				client.Connect(serverAddress);
				
				send(client);
				
				lobbyManager.StartHost();
			
			}
			catch (Exception)
			{
				Debug.Log("Failed to Connect to Server");
			}
			
		}

		void getResponse(Socket client) {

			byte[] messageBytes = new byte[2048];
			int messageInt = client.Receive(messageBytes);
			
			string messageString = System.Text.Encoding.ASCII.GetString(messageBytes,0,messageInt);

			Debug.Log(messageString);

			client.Close();
		}

		void send(Socket client) {

			string str = "text";

			byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(str);
			client.Send(toSendBytes);

			client.Close();
		}

		void connectToServer(){
			try
			{
				IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO1);

				Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				client.Connect(serverAddress);
				
				getResponse(client);
							
			}
			catch (Exception)
			{
				Debug.Log("Failed to Connect to Server");
			}
		}
	}
}