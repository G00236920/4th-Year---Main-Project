package ie.gmit.sw;

public class Runner
{
	
	 public static void main(String[] args) {
		 
		 Listener l1 = new Listener(5000);
		 Listener l2 = new Listener(5001);
		 
		 Thread t1 = new Thread(l1);
		 Thread t2 = new Thread(l2);
		 
		 t1.start();
	     t2.start();
	    
	 }

}

