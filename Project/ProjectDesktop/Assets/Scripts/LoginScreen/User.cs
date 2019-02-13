using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class User : MonoBehaviour {

   private static User _instance;

   public string username { get; set; }
   public string password { get; set; }

   public static User Instance
   {
      get { return _instance; }
   }

   private void Awake()
   {
      if (_instance != null && _instance != this)
      {
         Destroy(this.gameObject);
         return;
      }

      _instance = this;
      DontDestroyOnLoad(this.gameObject);
   }
}


