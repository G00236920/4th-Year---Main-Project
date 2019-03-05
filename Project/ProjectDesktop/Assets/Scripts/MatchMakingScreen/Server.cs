using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System;

//Serialisable object used for sending and receiving an object
//this object represents the Hosted game, bt username and the ipaddress of the host
[Serializable]
public class Server  {

	public string Ipaddress;
	public string Username;
	
}

