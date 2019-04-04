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
        string totVal = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//newList/Player";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
            

        foreach (XmlNode node in myNodeList)
        {
            XmlNode UserName = node.FirstChild;
            XmlNode Score = UserName.NextSibling;
            XmlNode Rank = Score.NextSibling;
 


            totVal += " " + UserName.InnerXml + " " + Score.InnerXml + " " + Rank.InnerXml;
            uiText.text = totVal;
        }
    }
}
