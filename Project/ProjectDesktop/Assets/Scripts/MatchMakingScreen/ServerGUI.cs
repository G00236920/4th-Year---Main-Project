using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGUI : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject ServerPanel;
    public GameObject ScrollArea;
    public GameObject ServerObject;
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

        int posY = 0;

        foreach (Server s in list)
        {
            GameObject child = Instantiate(ServerObject, new Vector3(0, posY, 0), Quaternion.identity);
            posY -= 108;
            child.transform.SetParent(ScrollArea.transform, false);
        }
	}

	public List<Server> getList(){
		return list;
	}
    
}