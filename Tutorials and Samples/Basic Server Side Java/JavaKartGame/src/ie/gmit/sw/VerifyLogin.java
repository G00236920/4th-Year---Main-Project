package ie.gmit.sw;

import org.bson.Document;

import com.mongodb.MongoClient;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import com.mongodb.client.model.Filters;

public class VerifyLogin implements DatabaseConnection{

	public void add(User user) {
<<<<<<< HEAD
		//HERE
=======
		//Add a new Account to the Database
>>>>>>> parent of d21432f4... Fixing incorrect login on server
		
	}
	
	public boolean findUserName(String username) {
		//returns true if the user is in the mongoDB
		MongoClient mongoClient = new MongoClient("127.0.0.1", 27017);
        
		MongoDatabase database = mongoClient.getDatabase("usersdb");
        
        MongoCollection<Document> collection = database.getCollection("users");
        Document myDoc = collection.find(Filters.eq("uname", username)).first();
        
        mongoClient.close();
        
        //If the User name is not in the Database
        if(myDoc == null)
        {
        	return false;
        }
        
		return true;
	}
	
	public boolean verifyPassword(String username, String password) {
		
		//returns true if the user is in the mongoDB
		MongoClient mongoClient = new MongoClient("127.0.0.1", 27017);
        
		MongoDatabase database = mongoClient.getDatabase("usersdb");
        
        MongoCollection<Document> collection = database.getCollection("users");
        Document myDoc = collection.find(Filters.eq("uname", username)).first();
        
        mongoClient.close();
        
        //If the User name is not in the Database
        if(myDoc == null)
        {
        	return false;
        }
        else if(myDoc.containsValue(password)) {
        	return true;
        }
        else {
        	return false;
        }

	}
	
}
