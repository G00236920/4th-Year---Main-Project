package ie.gmit.sw;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.net.ServerSocket;
import java.net.Socket;
 
public class Server
{
	 public static void main(String[] args) throws IOException {
		      
		      ServerSocket serverSocket = new ServerSocket(5000);
		      
		      System.out.println("Listening on port 5000");
		      
		      while (System.in.available() == 0) { 
		    	  
		    	  Socket socket = serverSocket.accept();
		    	  
		          System.out.println("A client has connected.");
		          
		          InputStream in = socket.getInputStream();
		          
		          try {
		        	  
		        	  User currentUser = (User) deSerialization(in);
		        	  
		        	  System.out.println("USERNAME: " +currentUser.getUsername()
		        	  					+"PASSWORD: " +currentUser.getPassword());
		        	  
					} catch (ClassNotFoundException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
		          
		          //Get Object being sent and De-serialize
		          
			      in.close();
		          socket.close();
		          
		       }
		      
		      serverSocket.close();
		      
		  }
	 
		public static Object deSerialization(InputStream in) throws IOException, ClassNotFoundException {

			BufferedInputStream bufferedInputStream = new BufferedInputStream(in);
			ObjectInputStream objectInputStream = new ObjectInputStream(bufferedInputStream);
			Object object = objectInputStream.readObject();
			objectInputStream.close();
			return object;
		}
	 
}

