using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGUI : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject ServerPanel;
	private static ServerGUI _instance;
    public static ServerGUI Instance { get { return _instance; } }
	private static List<Server> list;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

	public void CancelClicked()
    {
		ServerPanel.gameObject.SetActive (false);
        MainPanel.gameObject.SetActive (true);
	}

	public void RefreshClicked(){
		
	}

	public void setList(List<Server> l){ 
		list = l;
	}

	public List<Server> getList(){
		return list;
	}
    
}