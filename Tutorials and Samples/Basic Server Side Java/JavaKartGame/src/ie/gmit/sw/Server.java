package ie.gmit.sw;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
 
public class Server
{

    private static int portNumber = 5000;
 
    public static void main(String[] args)
    {
        try
        {

            System.out.println("Server Started and listening to the port 5000");
 
            while(true)
            {
                ServerSocket serverSocket = new ServerSocket(portNumber);
                Socket clientSocket = serverSocket.accept();
                
                PrintWriter out = new PrintWriter(clientSocket.getOutputStream(), true);
                BufferedReader in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
                
                System.out.println(out + " " + in);
                
            }
            

        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        
    }
}

