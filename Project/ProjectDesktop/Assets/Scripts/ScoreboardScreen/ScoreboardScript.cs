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
public class ScoreboardScript : MonoBehaviour
{
    //const int PORT_NO1 = 5005;
    //const int PORT_NO2 = 5006;

    //private IPAddress SERVER_IP = IPAddress.Parse("52.18.149.174");// ip of vm
                                                                   //private IPAddress SERVER_IP = IPAddress.Parse("127.0.0.1"); 

    public void ButtonClicked()
    {
        SendTest();

    }

    public static void SendTest()
    {
        List<Users> users = new List<Users>() {
    new Users() { Username = "Ray", Score = 10 },
    new Users() { Username = "John", Score = 20 },
    new Users() { Username = "Mike", Score = 30},
    new Users() { Username = "Flan", Score = 200},
    new Users() { Username = "Kevin", Score = 40},
    new Users() { Username = "boss", Score = 100000},
    new Users() { Username = "Sam", Score = 80}

    };// Users

       
        XDocument xdoc = new XDocument(
        new XDeclaration("1.0", "utf-8", "yes"),
        // This is the root of the document
        new XElement("ScoreList",
        from usr in users
        select
            new XElement("Player", new XAttribute("UserName", usr.Username),
            new XAttribute("Score", usr.Score)

            )));
        // xdoc.Save("ScoreList.xml"); // creates file in project/desktopProject
        Debug.Log("xdoc below");
        Debug.Log(xdoc);
        Debug.Log("string below");
        String doc = xdoc.ToString(); // converts doc to string
        Debug.Log(doc);
        String SERVER_IP = "52.18.149.174";// addresss of server
        Int32 Port = 5005;// port server is listening on

       
        TcpClient tcpClient = new TcpClient(SERVER_IP, Port);
        Debug.Log("Connected 1");
        // Uses the GetStream public method to return the NetworkStream.
        NetworkStream netStream = tcpClient.GetStream();
        Debug.Log("Connected 2");
        if (netStream.CanWrite)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes(doc);// converts doc to byte array
            netStream.Write(sendBytes, 0, sendBytes.Length);// sends to server
            Debug.Log("sent");

            StreamReader sr = new StreamReader(tcpClient.GetStream(), Encoding.ASCII);// receives data from server
            string received = sr.ReadToEnd(); // converts to string 

            XmlDocument xm = new XmlDocument();
            //XmlDeclaration xmldecl;
            //xmldecl = xm.CreateXmlDeclaration("1.0", "utf-8", "yes");
            //XmlElement root = xm.DocumentElement;
            Debug.Log("received below");
            Debug.Log(received);
            xm.LoadXml(received); // converts to xml
            Debug.Log("xm below");
            Debug.Log(xm);

            xm.Save("newList.xml");// saves xml file 
            //https://www.google.com/search?ei=BEavXPTbN8PhxgOMz4n4Ag&q=write+xml+file+unity+&oq=write+xml+file+unity+&gs_l=psy-ab.3..0i22i30.17146.23984..24516...0.0..0.117.1300.20j1......0....1..gws-wiz.......0i71j35i39j0i131j0j0i67j0i131i67j0i20i263._0tmMOCxygM#kpvalbx=1
            /*
            XmlSerializer serializer = new XmlSerializer(typeof(List<Users>), new XmlRootAttribute("Player"));
            FileStream stream = new FileStream(Application.dataPath + "scores.xml", FileMode.Create);
            serializer.Serialize(stream, xm);
            stream.Close();
            Debug.Log(Application.dataPath);
           */
            //string doc2 = xm.ToString();
            //Debug.Log(doc2.ToString());
            Debug.Log("1!!");
           // XmlSerializer serializer = new XmlSerializer(typeof(List<Users>), new XmlRootAttribute("Player"));
            Debug.Log("2!!");
            //StringReader stringReader = new StringReader(doc2);
           // stringReader.Read(); // skip BOM
            Debug.Log("3!!");
           // List<Users> Users = (List<Users>)serializer.Deserialize(stringReader);
            //Debug.Log("object!!");
            //Debug.Log(Users.ToString());
           // Debug.Log("object^^");
           // Debug.Log("1!!");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Users>),new XmlRootAttribute("ScoreList"));
          
           using (FileStream stream = File.OpenRead("newList.xml"))
           {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                Debug.Log("AHHHHH " + text);


                stream.Seek(0, SeekOrigin.Begin);//reset stream to start of  

                Debug.Log("4!!");
                List<Users> users2;

                users2 = (List<Users>)serializer.Deserialize(stream); 
                //List<Users> users2 = (List<Users>)serializer.Deserialize(stream);
                //List<Product> productList = (List<Product>)serializer.Deserialize(stringReader);
                Debug.Log(stream);
                Debug.Log("5!!");
              
               
                int size = users2.Count;
               Debug.Log("Count " + size);
               //Debug.Log("Users2 " + users2.Username.ToString());
                foreach ( Users UserName in  users2)
                {
                    Debug.Log("Username :"+UserName.Username);
                    Debug.Log("Rank :"+UserName.Rank);
                    Debug.Log("Score  :"+UserName.Score);
                    Debug.Log("6!!");
                }
                Debug.Log("7!!");
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

public class Users
{
    public string Username { get; set; }

    public int Score { get; set; }

    public int Rank { get; set; }

}//Users




