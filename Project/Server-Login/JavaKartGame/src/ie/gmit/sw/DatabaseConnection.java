package ie.gmit.sw;

public interface DatabaseConnection{
	
	public void add(String message);
	
	public boolean findUserName(String username);
	
	public boolean verifyPassword(String username, String password);
	

}
