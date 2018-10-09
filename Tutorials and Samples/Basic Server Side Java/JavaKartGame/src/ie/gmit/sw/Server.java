package ie.gmit.sw;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
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
			          
			          InputStream in = socket.getInputStream();
			          
			          BufferedReader br = new BufferedReader(new InputStreamReader(in));
			          
			          String request = null;
			          
				          while ((request = br.readLine()) != null) {
				        	  
				        	  System.out.println( request );
				        	  
				          }
			          
				      in.close();
			          socket.close();
			          
			       }
			      
			      serverSocket.close();
			      
			  }
	 
			}

