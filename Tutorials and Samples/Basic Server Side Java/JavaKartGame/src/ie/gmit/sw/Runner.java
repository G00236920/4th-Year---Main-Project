package ie.gmit.sw;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class Runner
{
	
	 public static void main(String[] args) {
		 
	     start(); 
	 }
	 
	 static void start() {
		    ServerSocket socket = null;
			
		    try {
				
				socket = new ServerSocket(5000);
			    
				System.out.println("Listening");
			      
			      while (true) {
			    	  
			         Socket sock = socket.accept();
			         System.out.println("Connected");
			         new Thread(new ServerConnection(sock)).start();
			        
			      }
			      
			} catch (IOException e) {

				e.printStackTrace();
			}
	 }
}

