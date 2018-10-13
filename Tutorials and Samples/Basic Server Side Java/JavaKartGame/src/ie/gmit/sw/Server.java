package ie.gmit.sw;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.BufferedReader;
import java.io.DataInputStream;
import java.net.ServerSocket;
import java.net.Socket;
 
public class Server
{
	 public static void main(String[] args) throws IOException, ClassNotFoundException {
		      
		      ServerSocket serverSocket = new ServerSocket(5000);
		      
		      System.out.println("Listening on port 5000");
		      
		      while (System.in.available() == 0) { 
		    	  
		    	  Socket socket = serverSocket.accept();
		    	  
		          System.out.println("A client has connected.");
		          
		          InputStream in = socket.getInputStream();
		          
		          BufferedReader incoming =
		        	        new BufferedReader(
		        	            new InputStreamReader(in));
				  
				  /*
				  User currentUser = (User)deSerialization(in);
				  
				  System.out.println("USERNAME: " +currentUser.getUsername()
				  					+"PASSWORD: " +currentUser.getPassword());
				  */
				  
				  
				  in.close();
		          
		          socket.close();
		          
		       }
		      
		      serverSocket.close();
		      
		  }
	 
		public static Object deSerialization(InputStream in) throws IOException, ClassNotFoundException {

			DataInputStream  input = new DataInputStream (in);
			
			Object object = input.read();
			
			System.out.println(object);
			
			input.close();
			
			return object;
		}
	 
}

