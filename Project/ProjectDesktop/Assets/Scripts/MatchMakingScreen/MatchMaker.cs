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

		const int PORT_NO1 = 5002;
		const int PORT_NO2 = 5003;
		private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");
		public LobbyManager lobbyManager;
		public GameObject mainPanel;
		public GameObject serverPanel;

		public void findGame() {

			connectToServer();
			
			mainPanel.SetActive(false);
			serverPanel.SetActive(true);

		}
		
		public void Host() {

			try {
				IPEndPoint serverAddress = new IPEndPoint(SERVER_IP, PORT_NO2);

				Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				client.Connect(serverAddress);
				
				send(client);
				
				lobbyManager.StartHost();
			
			}
			catch (Exception) {
				Debug.Log("Failed to Connect to Server");
			}
			
		}

		void getResponse(Socket client) {

			byte[] messageBytes = new byte[2048];
			int messageInt = client.Receive(messageBytes);
			
			string messageString = System.Text.Encoding.ASCII.GetString(messageBytes,0,messageInt);

			try {
				
				messageString = fixJson(messageString);
				ServerList result = JsonUtility.FromJson<ServerList>(messageString);
				ServerGUI.Instance.setList(result.list);

			}
			catch (Exception) {
				Debug.Log("Could Not get List of Servers ");
			}

			client.Close();
		}

		void send(Socket client) {

			byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(PlayerDetails.Instance.getUsername());
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

		string fixJson(string value)
		{
			value = "{\"list\":" + value + "}";
			return value;
		}
	}
}