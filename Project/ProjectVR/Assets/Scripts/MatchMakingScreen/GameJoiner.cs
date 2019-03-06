using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class GameJoiner : MonoBehaviour {

	public string Ipaddress;
	public LobbyMainMenu lobbyMenu;

	// Use this for initialization
	void Start () {
		lobbyMenu = transform.root.GetComponent<LobbyMainMenu>();
	}

	public void OnClickJoin()
	{
			//This method is called when the user clicks to join a hosted game
			//sends the ipaddress as a parameter
			lobbyMenu.OnClickJoin(Ipaddress);

	}

}
