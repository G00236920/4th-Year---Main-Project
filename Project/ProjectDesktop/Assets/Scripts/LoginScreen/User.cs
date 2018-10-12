using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable()]
public class User : ISerializable {

   string username {set; get;}
   string password {set; get;}

   public User(string name, string pass)
   {
	   this.username = name;
	   this.password = pass;
   }

	//Serialize
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Username", username);
		info.AddValue("Password", password);
    }

	//Deserialize
	public User (SerializationInfo info, StreamingContext context){
		username = (string)info.GetValue("Name", typeof(string));
		password = (string)info.GetValue("Password", typeof(string));
	}

}
