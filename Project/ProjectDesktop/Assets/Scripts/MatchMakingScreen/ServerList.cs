using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerList : MonoBehaviour {

	public GameObject MainPanel;
    public GameObject ServerPanel;
	private static ServerList _instance;
    public static ServerList Instance { get { return _instance; } }
	public List <Server> listOfServers = new List<Server>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

	public List<Server> getList(){
		return listOfServers;
	}

	public void setList(List<Server> list){
		listOfServers = list;
	}

}
