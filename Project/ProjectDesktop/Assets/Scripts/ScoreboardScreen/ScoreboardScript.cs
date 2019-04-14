using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class ScoreboardScript : MonoBehaviour
{

    public Text dets1Text;
    //public Text dets2Text;
    
    public void ButtonClicked()
    {
        SendTest();

    }

    public  void SendTest()
    {
        //RectTransform rectTransform;
        //rectTransform = dets1Text.GetComponent<RectTransform>();
        //rectTransform.localPosition = new Vector3(0, 0, 0);
        //rectTransform.sizeDelta = new Vector2(600, 200);

        List<Users> users = new List<Users>() {
    new Users() { Username = "Ray", Score = 10 },
    new Users() { Username = "John", Score = 20 },
    new Users() { Username = "Mike", Score = 30},
    new Users() { Username = "Flan", Score = 200},
    new Users() { Username = "Kevin", Score = 40}
    //new Users() { Username = "boss", Score = 100000},
    // new Users() { Username = "Sam", Score = 80}

    };// Users

        foreach (Users UserName in users)
        {
            Debug.Log(UserName.Username + "          " + UserName.Score);
       
            dets1Text.text += "    "+ UserName.Username + "       " + UserName.Score+ "\n";
          
        }// prints object to cnsole for debug purposes

        //Debug.Log(Username);
        int size = users.Count;
        Debug.Log("Count of first object : " + size);// prints count of object to console for debug purposes

        XDocument xdoc = new XDocument(
        new XDeclaration("1.0", "utf-8", "yes"),
        // This is the root of the document
        new XElement("ScoreList",
        from usr in users
        select
            new XElement("Player", new XAttribute("UserName", usr.Username),
            new XAttribute("Score", usr.Score)

            )));// creates xml of object for sending to database
        // xdoc.Save("ScoreList.xml"); // creates file in project/desktopProject

        String doc = xdoc.ToString(); // converts doc to string

        String SERVER_IP = "52.18.149.174";// addresss of server
        Int32 Port = 5005;// port server is listening on


        TcpClient tcpClient = new TcpClient(SERVER_IP, Port);

        // Uses the GetStream public method to return the NetworkStream.
        NetworkStream netStream = tcpClient.GetStream();

        if (netStream.CanWrite)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes(doc);// converts doc to byte array
            netStream.Write(sendBytes, 0, sendBytes.Length);// sends to server


            StreamReader sr = new StreamReader(tcpClient.GetStream(), Encoding.ASCII);// receives data from server
            string received = sr.ReadToEnd(); // converts to string 

            XmlDocument xm = new XmlDocument();

            xm.LoadXml(received); // converts to xml

            xm.Save("newList.xml");// saves xml file 
                                   //https://www.google.com/search?ei=BEavXPTbN8PhxgOMz4n4Ag&q=write+xml+file+unity+&oq=write+xml+file+unity+&gs_l=psy-ab.3..0i22i30.17146.23984..24516...0.0..0.117.1300.20j1......0....1..gws-wiz.......0i71j35i39j0i131j0j0i67j0i131i67j0i20i263._0tmMOCxygM#kpvalbx=1


            XmlSerializer serializer = new XmlSerializer(typeof(List<Users>), new XmlRootAttribute("ScoreList"));

            using (FileStream stream = File.OpenRead("newList.xml"))// opens file 
            {
                //StreamReader reader = new StreamReader(stream);// reads in file
                //string text = reader.ReadToEnd();//reads to end of file



                stream.Seek(0, SeekOrigin.Begin);//resets stream to start of file  

                List<Users> users2 = (List<Users>)serializer.Deserialize(stream);// converts xml back to an object
                Debug.Log("========================");
                int size2 = users2.Count;
                Debug.Log("Count of new object with rank : " + size2);

                foreach (Users UserName in users2)
                {
                   
                    Debug.Log("Username : " + UserName.Username + " Rank : " + UserName.Rank + " Score  : " + UserName.Score);

                    //dets2Text.text += UserName.Username + " Rank : " + UserName.Rank + " Score  : " + UserName.Score;
                    
                }// prints object to cnsole for debug purposes

            }

        }// if


        else
        {
            Debug.Log("You cannot write data to this stream.");
            tcpClient.Close();
            Debug.Log("tcp closed");
            // Closing the tcpClient instance does not close the network stream.
            netStream.Close();
            Debug.Log("netstream closed");
            return;
        }

        Debug.Log("connection closed");

    }//SentTest


}// ScoreBoard
 [Serializable]
public class Users
{
    public string Username { get; set; }

    public int Score { get; set; }

    public int Rank { get; set; }

}//Users




