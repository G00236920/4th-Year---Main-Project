using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System;

[System.Serializable()]
public class ServerList {
    
	public List<Server> list;
	
}

[Serializable]
public class Server  {

	public string Ipaddress {set; get;}
	public string Username {set; get;}
	
}
