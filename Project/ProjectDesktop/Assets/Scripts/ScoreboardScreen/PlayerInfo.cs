using System.Linq;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public TextAsset xmlRawFile;
    public Text uiText;
   void Start()
    {
        string data = xmlRawFile.text;
        parseXmlFile(data);
    }

    void parseXmlFile(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//ScoreList/Player";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);



    }
}
