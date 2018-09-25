using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System.IO;

public class ConnectionToJava : MonoBehaviour
{

    //Use port 5000
    private const int port = 5000;
    //Using UDP Client
    private UdpClient client;
    //Use the Ip and port as End Point
    private IPEndPoint ep;


    void Start()
    {

        try
        {
            TcpClient tcpclnt = new TcpClient();
            Debug.Log("Connecting.....");

            tcpclnt.Connect("192.168.0.100", 8001);
            // use the ipaddress as in the server program

            Debug.Log("Connected");

            Stream stm = tcpclnt.GetStream();

            ASCIIEncoding message = new ASCIIEncoding();
            byte[] ba = message.GetBytes("Hello from C#");
            Debug.Log("Transmitting.....");

            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);

            for (int i = 0; i < k; i++)
                Debug.Log(Convert.ToChar(bb[i]));

            tcpclnt.Close();
        }

        catch (Exception e)
        {
            Debug.Log("Error..... " + e.StackTrace);
        }

    }


}
