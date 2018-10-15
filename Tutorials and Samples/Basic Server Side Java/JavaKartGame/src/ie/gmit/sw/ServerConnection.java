package ie.gmit.sw;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

import com.google.gson.Gson;

public class ServerConnection implements Runnable {

	Socket csocket;

	ServerConnection(Socket csocket) {
		this.csocket = csocket;
	}

	public void run() {
		

		try {
			
	        InputStream is = csocket.getInputStream();
	        OutputStream os = csocket.getOutputStream();
	        
	        //Receive Login info from unity
	        String received = receiveMessage(is);
	        //Convert login info to a User object
	        User user = getUserFromJson(received);
	        
	        System.out.println(user.getUsername()+" " +user.getPassword());
	        //Send a Reply to the Unity Client
	        respond(os);
			
	        //Close Streams and Sockets
			is.close();
			os.close();
			csocket.close();

		} catch (IOException e) {
			System.out.println(e);
		}
	}

	public static User getUserFromJson(String response) {

		Gson gson = new Gson();
		User user = gson.fromJson(response, User.class);

		return user;

	}

	public static void respond(OutputStream os) {
		
        String toSend = "Echo: ";
        byte[] toSendBytes = toSend.getBytes();
        int toSendLen = toSendBytes.length;
        byte[] toSendLenBytes = new byte[4];
        toSendLenBytes[0] = (byte)(toSendLen & 0xff);
        toSendLenBytes[1] = (byte)((toSendLen >> 8) & 0xff);
        toSendLenBytes[2] = (byte)((toSendLen >> 16) & 0xff);
        toSendLenBytes[3] = (byte)((toSendLen >> 24) & 0xff);
        
        try {
			os.write(toSendLenBytes);
	        os.write(toSendBytes);
		} catch (IOException e) {
			e.printStackTrace();
		}
        
	}
	
	public static String receiveMessage(InputStream is) {
		
		byte[] receivedBytes = null;
		int len = 0;
		
        try {
            byte[] lenBytes = new byte[4];
			is.read(lenBytes, 0, 4);
			len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
						((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
	        receivedBytes = new byte[len];
	        is.read(receivedBytes, 0, len);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        
        return new String(receivedBytes, 0, len);
        
	}

}
