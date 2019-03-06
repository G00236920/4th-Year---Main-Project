using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyServerEntry : MonoBehaviour 
    {
        public Text serverInfoText;
        public Text slotInfo;
        public Button joinButton;

		public void Populate(MatchInfoSnapshot match, LobbyManager lobbyManager, Color c)
		{
            //set the name of the match
            serverInfoText.text = match.name;
            //show the number of players that can play and the number that are currently playing
            slotInfo.text = match.currentSize.ToString() + "/" + match.maxSize.ToString(); ;
            //Network ID for use with the UNET system
            NetworkID networkID = match.networkId;
            //Limit the listeners to only one function
            joinButton.onClick.RemoveAllListeners();
            joinButton.onClick.AddListener(() => { JoinMatch(networkID, lobbyManager); });

            GetComponent<Image>().color = c;
        }

        void JoinMatch(NetworkID networkID, LobbyManager lobbyManager)
        {
            //allow the player to join a match
			lobbyManager.matchMaker.JoinMatch(networkID, "", "", "", 0, 0, lobbyManager.OnMatchJoined);
			lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();
        }
    }
}