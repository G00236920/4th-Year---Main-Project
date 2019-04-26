using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;


public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject Kart;
    public GameObject Bike;
    public GameObject Ball;
    public GameObject straight;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject startPos;
    public GameObject finish;
    GameObject myPlayerUnit;
    
    private List<Vector3> SpawnPoints = new List<Vector3>();

    // Use this for initialization
    void Start()
    {

        //Collection of Spawn Points, Depending on how many people are playing
        SpawnPoints.Add(new Vector3(-4.61f, 0.91f, 1.9f));
        SpawnPoints.Add(new Vector3(4.9f, 0.91f, 1.9f));
        SpawnPoints.Add(new Vector3(-4.61f, 0.91f, 8.44f));
        SpawnPoints.Add(new Vector3(4.9f, 0.91f, 8.44f));

        if (!isLocalPlayer)
        {
            return;
        }

        if (isServer)
        {
            if (connectionToClient.isReady)
            {
                CmdSpawnTrack();
            }
            else {
                StartCoroutine(WaitForTrack());
            }
        }

        Camera.main.gameObject.SetActive(false);
        PlayerDetails.Instance.setSpawnPos(SpawnPoints[PlayerDetails.Instance.getPos()-1], new Quaternion(0f, 0f , 0f, 1));
        CmdSpawnMyBike(SpawnPoints[PlayerDetails.Instance.getPos()-1]);
    }


    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (OVRInput.Get(OVRInput.RawButton.B))
        {
            CmdDestroyMyUnit();
            CmdSpawnMyBike(PlayerDetails.Instance.getSpawnPos());
        }


    }


    public void Respawn() {
        //Destroy the Unit 
        CmdDestroyMyUnit();
        //Respawn the kart
        CmdSpawnMyBike(PlayerDetails.Instance.getSpawnPos());
    }

    [Command]
    public void CmdDestroyUnit(GameObject player) {
        Destroy(player);
    }

    //Commands - only executed on the server
    [Command]
    public void CmdSpawnMyKart(Vector3 pos)
    {
        //creates the object on the server
        GameObject go = Instantiate(Kart, pos, Quaternion.identity);
        
        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);

    }

    [Command]
    public void CmdSpawnMyBike(Vector3 pos)
    {

        if (connectionToClient.isReady)
        {
            //creates the object on the server
            GameObject go = Instantiate(Bike, pos, Quaternion.identity);

            myPlayerUnit = go;

            //propagate the object to all clients
            NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);

        }
        else {
            StartCoroutine(WaitForReady());
        }
    }

    [Command]
    public void CmdSpawnMyBall()
    {
        //creates the object on the server
        GameObject go = Instantiate(Ball);

        myPlayerUnit = go;

        //propagate the object to all clients
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);

    }

    [Command]
    public void CmdDestroyMyUnit()
    {
        //Destroy the Current version of the players Unit
        Destroy(myPlayerUnit);

    }

    IEnumerator WaitForTrack()
    {
        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }
        CmdSpawnTrack();
    }
	
	[Command]
    public void CmdSpawnTrack()
    {
		
		GameObject lastpiece = null;
        lastpiece =  CmdSpawnPiece(startPos, null);

        int r = 0;
        int l = 0;
        
		for(int i = 0; i < 100; i++){

			switch(Random.Range(0, 5)){
				case 1:
                    lastpiece = CmdSpawnPiece(up, lastpiece);
				break;
				case 2:
                    lastpiece = CmdSpawnPiece(down, lastpiece);
				break;
				case 3:
                    if(r == 0){
                        lastpiece = CmdSpawnPiece(right, lastpiece);
                        r = 1;
                        l = 0;
                    }
				break;
				case 4:
                    if(l == 0){
                        lastpiece = CmdSpawnPiece(left, lastpiece);
                        l = 1;
                        r = 0;
                    }
				break;
				default:
                    lastpiece = CmdSpawnPiece(straight, lastpiece);
				break;
			}

		}

        lastpiece = CmdSpawnPiece(finish, lastpiece);
    }

    public GameObject CmdSpawnPiece(GameObject currentPiece, GameObject lastpiece){
        GameObject go = null;
        
        if(lastpiece == null){
            go = Instantiate(currentPiece, new Vector3(0f,0f,0f), new Quaternion(0,0,0,0));
        }
        else{
            go = Instantiate(currentPiece, lastpiece.transform.GetChild(2).position, lastpiece.transform.GetChild(2).rotation);
        }
        
        //propagate the object to all clients
        NetworkServer.Spawn(go);

        return go;
    }

    IEnumerator WaitForReady()
    {
        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }

        CmdSpawnMyBike(PlayerDetails.Instance.getSpawnPos());

    }

}
