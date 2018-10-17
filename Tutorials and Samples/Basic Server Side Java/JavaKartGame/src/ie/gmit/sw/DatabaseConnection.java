package ie.gmit.sw;

public interface DatabaseConnection{
	
	public void add(User user);
	
	public boolean findUserName(String username);
	
	public boolean verifyPassword(String username, String password);
	

}
