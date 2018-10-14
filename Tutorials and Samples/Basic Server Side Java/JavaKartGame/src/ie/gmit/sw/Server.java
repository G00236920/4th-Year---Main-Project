package ie.gmit.sw;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.BufferedReader;
import java.net.ServerSocket;
import java.net.Socket;

import com.google.gson.Gson;
 
public class Server
{
	 public static void main(String[] args) throws IOException, ClassNotFoundException {
		      
		      ServerSocket serverSocket = new ServerSocket(5000);
		      
		      System.out.println("Listening on port 5000");
		      
		      while (System.in.available() == 0) { 
		    	  
		    	  Socket socket = serverSocket.accept();
		    	  
		          System.out.println("A client has connected.");
		          
		          InputStream in = socket.getInputStream();
		          OutputStream out = socket.getOutputStream();
		          
		          StringBuilder sb = new StringBuilder();

		          String response;
		          
		          BufferedReader br = new BufferedReader(new InputStreamReader(in));
		          
		          while ((response = br.readLine()) != null) {
		        	  
		              sb.append(response);
		              
		          }
          
		          User currentUser = getUserFromJson(sb.toString());
		          
		          Boolean found = isUserInDatabase(currentUser);
		          
		          //convert the boolean to byte array
		          byte [] result = new byte[]{(byte) (found?1:0)};

		          //Send the Response back to Unity
		          out.write(result, 0, result.length);
		          
		          System.out.println("Response Sent");
				  
		          //Close the streams
				  in.close();
				  out.close();
				  
		          //Close the sockets
		          socket.close();
		          
		       }
		      
		      serverSocket.close();
		      
	 }
	 
	 public static User getUserFromJson(String response) {
		 
			 Gson gson = new Gson();
			 User user = gson.fromJson(response, User.class);  
			
			return user;
	 }
	 
	 public static boolean isUserInDatabase(User user) {
		 
		 boolean found = true;
		 
		 if(found) {
			 found = isPasswordCorrect(user);
		 }
		 else {
			 found = false;
		 }
		 
		return found;

	 }
	 
	 public static boolean isPasswordCorrect(User user) {
		 
		 	boolean found = true;
		 	//Check if the password matches the user name
		 	return found;
	 }
	 
}

