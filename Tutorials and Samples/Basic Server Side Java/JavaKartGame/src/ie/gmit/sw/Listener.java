package ie.gmit.sw;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class Listener implements Runnable{
	
	int portNo;
	
	Listener(int portNo) {
		this.portNo = portNo;
	}

	public void run() {
		
	    ServerSocket socket = null;
		
	    try {
	    	System.out.println("Listening on Port "+this.portNo);
			socket = new ServerSocket(this.portNo);
		    
		      
		      while (true) {
		    	  
		         Socket sock = socket.accept();
		         new Thread(new ServerConnection(sock)).start();
		        
		      }
		      
		} catch (IOException e) {

			e.printStackTrace();
		}
	}

}
