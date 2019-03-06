using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerGUI : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject ServerPanel;
    public GameObject ScrollArea;
    public GameObject ServerObject;
    public GameObject MatchController;

	private static ServerGUI _instance;
    public static ServerGUI Instance { get { return _instance; } }
	private static List<Server> list;


    private void Awake()
    {
        //used as a singleton
        if (_instance != null && _instance != this)
        {
            //destroy the object if an instance of it exists already
            Destroy(this.gameObject);
        } 
        else 
        {
            //set the only instance of the object
            _instance = this;
        }
    }

	public void CancelClicked()
    {
        //if the cancel button is clicked, then show the main panel
        //hide the server list panel
		ServerPanel.gameObject.SetActive (false);
        MainPanel.gameObject.SetActive (true);
	}

	public void RefreshClicked(){
        //get the findGame method from our controller
        MatchController.GetComponent<Prototype.NetworkLobby.MatchMaker>().findGame();
	}

	public void setList(List<Server> l){
        //set the list of servers
        list = l;

        //used to position the list, item by item on the screen
        int posY = 0;

        foreach (Server server in l)
        {
            //for each server in the list of servers, give a button and a username
            //set the position of the canvas item , based on the previous item
            GameObject child = Instantiate(ServerObject, new Vector3(0, posY, 0), Quaternion.identity);
            posY -= 108;
            child.transform.SetParent(ScrollArea.transform, false);
            child.transform.Find("PlayerName").gameObject.GetComponent<Text>().text = server.Username;
            child.transform.GetComponent<GameJoiner>().Ipaddress = server.Ipaddress;
        }
	}

	public List<Server> getList(){
        //get the list of servers
		return list;
	}

}