package ie.gmit.sw;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
 
public class Server
{
	 public static void main(String[] args)
	 
			 throws IOException {
			      
			      ServerSocket serverSocket = new ServerSocket(5000);
			      
			      System.out.println("Listening on port 5000");
			      
			      while (System.in.available() == 0) { 
			    	  
			    	  Socket socket = serverSocket.accept();
			    	  
			          System.out.println("A client has connected.");
			          
			          socket.close();
			          
			       }
			      
			      serverSocket.close();
			      
			  }
			}

