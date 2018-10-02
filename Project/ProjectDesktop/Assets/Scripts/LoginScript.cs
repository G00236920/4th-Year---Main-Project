using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class LoginScript : MonoBehaviour {

    [SerializeField]
    private string test;

    //Use port 5000
    private const int port = 5000;
    //Using UDP Client
    private UdpClient client;
    //Use the Ip and port as End Point
    private IPEndPoint ep;


    public void ButtonClicked()
    {

        try
        {
            TcpClient tcpclnt = new TcpClient();
            Debug.Log("Connecting.....");

            tcpclnt.Connect("127.0.0.1", 5000);
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

        catch (Exception)
        {
            Debug.Log("Failed to Connect to Java");
        }

    }


}
