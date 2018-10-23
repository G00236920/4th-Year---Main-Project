using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerListScript : MonoBehaviour {

	public GameObject MainPanel;
    public GameObject ServerPanel;

	public void CancelClicked(){
        MainPanel.gameObject.SetActive (true);
        ServerPanel.gameObject.SetActive (false);
	}

	public void RefreshClicked(){

	}

}
