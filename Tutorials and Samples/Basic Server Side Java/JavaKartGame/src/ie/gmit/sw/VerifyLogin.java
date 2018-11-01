package ie.gmit.sw;

import org.bson.Document;

import com.mongodb.BasicDBObject;
import com.mongodb.Cursor;
import com.mongodb.DB;
import com.mongodb.DBCollection;
import com.mongodb.DBCursor;
import com.mongodb.MongoClient;
import com.mongodb.client.FindIterable;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoCursor;
import com.mongodb.client.MongoDatabase;
import com.mongodb.client.MongoIterable;
import com.mongodb.client.model.Filters;

public class VerifyLogin implements DatabaseConnection{

	public void add(User user) {
		//Add a new Account to the Database
		
	}
	
	public boolean findUserName(String username) {
		//returns true if the user is in the mongoDB
		MongoClient mongoClient = new MongoClient("127.0.0.1", 27017);
        
		MongoDatabase database = mongoClient.getDatabase("usersdb");
        
        MongoCollection<Document> collection = database.getCollection("users");
        Document myDoc = collection.find(Filters.eq("uname", username)).first();
        
        //System.out.println(myDoc);
        if(myDoc == null) 
        {
        	return false;
        }
		return true;
	}
	
	public boolean verifyPassword(String username, String password) {
		//returns true if the password matches the User in the mongoDB
		
		return true;
	}
	
}
