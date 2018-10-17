package ie.gmit.sw;

import com.mongodb.MongoClient;

public final class Database {
	
    private static volatile MongoClient mongoClient = new MongoClient( "host1" , 27017 );

    private Database() {}

    public static MongoClient getInstance() {
    	
        if (mongoClient == null) {
            
        	synchronized(Database.class) {
                
            	if (mongoClient == null) {
                	
                	mongoClient = new MongoClient();
                	
                }
            }
        }
        
        return mongoClient;
    }
}
