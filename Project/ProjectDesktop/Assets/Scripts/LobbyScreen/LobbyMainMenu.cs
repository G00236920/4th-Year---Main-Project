using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{   
    //Originally retrieve this code from the unity store and adapted it to fit our purposes
     
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;
        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public InputField ipInput;
        public InputField matchNameInput;


        public void OnEnable()
        {
            //show the top panel.
            lobbyManager.topPanel.ToggleVisibility(true);
            //Remove all listeners
            ipInput.onEndEdit.RemoveAllListeners();
            //add a listener when the user interacts with the IP box
            ipInput.onEndEdit.AddListener(onEndEditIP);
            
        }

        public void OnClickJoin(string ip)
        {
            //Change the canvas to lobby panel
            lobbyManager.ChangeTo(lobbyPanel);
            
            //if an ip address is called as a parameter of this method assume
            //that the connection should be made with this ip
            //if not then use the IP address typed in by the user
            if(ip != null){
                lobbyManager.networkAddress = ip;
            }
            else{
                //this will retreive the text from the input field
                //the user can enter any ip address here.
                //must enter a local network IP address if on the same network
                lobbyManager.networkAddress = ipInput.text;
            }
            
            //Start a client connection
            lobbyManager.StartClient();

            //stops the client from using callback later
            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            //show a connecting menu, so the user knows its connecting
            //This will fail or timeout if the ipaddress is abandoned or incorrect
            lobbyManager.DisplayIsConnecting();
            //connect using the network address
            lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }

        //set the incoming ip address as null
        //could be done in another way such as passing a value, but null works fines
        void onEndEditIP(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickJoin(null);
            }
        }

//Un needed code     
//These methods where part of the original code but we bypassed them and have our
//own as these did not fit our purpose.

/*
        public void OnClickCreateMatchmakingGame()
        {

            lobbyManager.StartMatchMaker();
            lobbyManager.matchMaker.CreateMatch(
                matchNameInput.text,
                (uint)lobbyManager.maxPlayers,
                true,
				"", "", "", 0, 0,
				lobbyManager.OnMatchCreate);

            lobbyManager.backDelegate = lobbyManager.StopHost;
            lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Matchmaker Host", lobbyManager.matchHost);
        }

        public void OnClickOpenServerList()
        {
            lobbyManager.StartMatchMaker();
            lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
            lobbyManager.ChangeTo(lobbyServerList);
        }

        void onEndEditGameName(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickCreateMatchmakingGame();
            }
        }
*/

    }
}
