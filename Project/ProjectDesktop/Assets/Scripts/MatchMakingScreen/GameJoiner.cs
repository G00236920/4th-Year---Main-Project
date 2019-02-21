using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class GameJoiner : MonoBehaviour {

	public string Ipaddress;
	public LobbyManager lobbyManager;
	public RectTransform lobbyPanel;

	// Use this for initialization
	void Start () {
		lobbyManager = transform.root.GetComponent<LobbyManager>();
	}

	public void OnClickJoin()
	{

		lobbyManager.ChangeTo(lobbyPanel);

		lobbyManager.networkAddress = Ipaddress;
		lobbyManager.StartClient();

		lobbyManager.backDelegate = lobbyManager.StopClientClbk;
		lobbyManager.DisplayIsConnecting();

		lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
	
	}

}
