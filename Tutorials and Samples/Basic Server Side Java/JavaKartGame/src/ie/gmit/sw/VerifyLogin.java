package ie.gmit.sw;

import org.bson.Document;

import com.mongodb.BasicDBObject;
import com.mongodb.MongoClient;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;

public class VerifyLogin implements DatabaseConnection{

	public void add(User user) {
		//Add a new Account to the Database
		
	}
	
	public boolean findUserName(String username) {
		//returns true if the user is in the mongoDB
		MongoClient mongoClient = new MongoClient("127.0.0.1", 27017);
        
        MongoDatabase database = mongoClient.getDatabase("usersdb");
        
        
        BasicDBObject searchQuery = new BasicDBObject();
        MongoCollection<Document> collection = database.getCollection("users");
        
		return true;
	}
	
	public boolean verifyPassword(String username, String password) {
		//returns true if the password matches the User in the mongoDB
		
		return true;
	}
	
}
