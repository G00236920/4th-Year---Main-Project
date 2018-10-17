package ie.gmit.sw;

public class VerifyLogin implements DatabaseConnection{

	public void add(User user) {
		//Add a new Account to the Database
		
	}
	
	public boolean findUserName(String username) {
		//returns true if the user is in the mongoDB
		Database.getInstance();
		
		return true;
	}
	
	public boolean verifyPassword(String username, String password) {
		//returns true if the password matches the User in the mongoDB
		
		return true;
	}
	
}
