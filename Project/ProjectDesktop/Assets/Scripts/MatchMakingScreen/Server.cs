using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Runtime.Serialization;

[System.Serializable()]
public class Server : ISerializable {

private IPAddress ip {set; get;}
private string username {set; get;}

	public Server(IPAddress address, string name){
		ip = address;
		username = name;
	}

	//Serialize
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("ipAddress", ip);
		info.AddValue("Username", username);
    }

	//Deserialize
	public Server (SerializationInfo info, StreamingContext context){
		ip = (IPAddress)info.GetValue("ipAddress", typeof(IPAddress));
		username = (string)info.GetValue("Username", typeof(string));
	}
	
}


