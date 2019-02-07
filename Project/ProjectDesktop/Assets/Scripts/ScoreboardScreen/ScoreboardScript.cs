using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardScript : MonoBehaviour {

    public void ButtonClicked()
    {
        CreateXml();

    } //ButtonClicked 

    private static void CreateXml()
    {
        var players = new List<Player>() {
    new Player() { Username = "Ray", Score = 10 },
    new Player() { Username = "John", Score = 20 },
    new Player() { Username = "Mike", Score = 30},
    new Player() { Username = "Flan", Score = 200},
    new Player() { Username = "Kevin", Score = 40}
    };// player

        // SceneManager.LoadScene("2.Lobby", LoadSceneMode.Single);
        XDocument xdoc = new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
              // This is the root of the document
              new XElement("Scores",
              from ply in players
              select
                  new XElement("Scores", new XAttribute("UserName", ply.Username),
                  new XElement("Score", ply.Score)

                 )));

        xdoc.Save("Scores.xml"); // creates file in project/desktopProject

    }
}
public class Player
{
    public string Username { get; set; }
    
    public int Score { get; set; }

   
    
}

