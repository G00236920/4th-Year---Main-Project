using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System;

//used to retreive the Json list as a list of servers
//json is not a big fan of listed serialised object, so better to make the list itself a serialisable object
[System.Serializable()]
public class ServerList {
    
	public List<Server> list;
	
}