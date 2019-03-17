using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

	public GameObject straight;
	public GameObject up;
	public GameObject down;
	public GameObject left;
	public GameObject right;

	private List<GameObject> trackList;


	// Use this for initialization
	void Start () {
		
		int r = 0; 
		int l = 0;
		
		GameObject lastpiece = Instantiate(straight, new Vector3(0, 0, 0),  Quaternion.identity);
		
		for(int i = 0; i < 100; i++){

			GameObject currentPiece = null;

			switch(Random.Range(0, 5)){
				case 1:
					currentPiece = Instantiate(up, lastpiece.transform.GetChild(2).position, lastpiece.transform.GetChild(2).rotation);
				break;
				case 2:
					currentPiece = Instantiate(down, lastpiece.transform.GetChild(2).position, lastpiece.transform.GetChild(2).rotation);
				break;
				case 3:
					if(l == 1){
						i--;
						continue;
					}
					else{
						currentPiece = Instantiate(left, lastpiece.transform.GetChild(2).position, lastpiece.transform.GetChild(2).rotation);
						r = 0;
						l = 1;
					}
				break;
				case 4:
					if(r == 1){
						i--;
						continue;
					}
					else{
						currentPiece = Instantiate(right, lastpiece.transform.GetChild(2).position, lastpiece.transform.GetChild(2).rotation);
						l = 0;
						r = 1;
					}
				break;
				default:
					currentPiece = Instantiate(straight, lastpiece.transform.GetChild(2).position, lastpiece.transform.GetChild(2).rotation);
				break;
			}

			lastpiece = currentPiece;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
