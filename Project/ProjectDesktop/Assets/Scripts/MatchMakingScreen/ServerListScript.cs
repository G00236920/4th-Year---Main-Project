using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerListScript : MonoBehaviour {

	public GameObject MainPanel;
    public GameObject ServerPanel;

	public void CancelClicked(){
		ServerPanel.gameObject.SetActive (false);
        MainPanel.gameObject.SetActive (true);
	}

	public void RefreshClicked(){

	}

}
