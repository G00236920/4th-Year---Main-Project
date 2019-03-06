using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//User object that can be serialised and sent over the server socket
//or can be cnovert from json sent back from the server
[System.Serializable]
public class User  {

   public string username;
   public string password;
}