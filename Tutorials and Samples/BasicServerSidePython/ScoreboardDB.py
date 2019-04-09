#python 3.5  other versions can't use mysql.connector without getting error ScoreboardDB
import mysql.connector
import socket
import xml.etree.cElementTree as etree
import xml.etree.ElementTree as ET
from xml.etree.ElementTree import tostring
from mysql.connector.cursor import MySQLCursorPrepared
from io import StringIO
import time
from io import BytesIO
  
def readXML(tree):
    playerNames = []
    playerScore = []
   # takes the xml passed from the Client Connection file and passses UserName & Score
   # in to the lists one called playerNames and the other called playerScore
    for player in tree.findall('Player'):
            score = player.get('Score')
            name = player.get('UserName')
            playerNames.append(name )
            playerScore.append (score)
    # calls the writeToDB & passes playerNames,playerScore
    writeToDB(playerNames,playerScore)
    
def writeToDB(playerNames,playerScore):
    try:
        # connects to the mariaDB using the login info below 
        con = mysql.connector.connect(port=5004,user='root',password='password',host='localhost',database='scoreboard')
        # records_to_insert is a list of tuples of the players info 
        records_to_insert = [ (playerNames[0],playerScore[0]) ,
                            (playerNames[1],playerScore[1]),
                            (playerNames[2],playerScore[2]) ,
                            (playerNames[3],playerScore[3]) 
                            ]
        # The sql query to run with %s used instead of the variables (playerNames ,playerScore )
        # Query also checks if the player name exists in the DB that the score should be added to there exisiting score
        # If the playerName doesnt exist it adds it to the DB 
        sql_insert_query = " INSERT INTO results (Player, Score) VALUES (%s,%s) ON DUPLICATE KEY UPDATE Score = VALUES(Score) + Score ; "
        # Connects to DB 
        myCursor = con.cursor()
        # Executes the SQL query with the player info 
        myCursor.executemany(sql_insert_query, records_to_insert)
        # Commits the sql query. If not used changes to DB won't be saved.  
        con.commit()
 
       # print (myCursor.rowcount, "Record inserted successfully into scoreboard results table")
        
    except mysql.connector.Error as error :
        #Prints if the DB doesnt connect and prints the error 
        print("Failed inserting record into results table {}".format(error))
    #Closes the connection
    myCursor.close()
    con.close()
    #Calls the rankDB and passes the list playerNames
    rankDB(playerNames)
    
def rankDB(playerNames):
    playerRank = []
    #creates a MySQLConnection with the port, userName, password, host and Db name stored in it. 
    con = mysql.connector.connect(port=5004,user='root',password='password',host='localhost',database='scoreboard')

    #passes the sql connection to myCursor 
    cursor = con.cursor(buffered=True)
    # passes this query to the DB and stores results in rank 
    cursor.execute ("SELECT Player, Score ,RANK() OVER (ORDER BY Score DESC) world_rank FROM results ;")
    rank = cursor.fetchall() 

    #adds the players rank to the playerRank list. 
    index = [x for x, y in enumerate(rank) if y[0] == playerNames[0]]
    index = int("".join(map(str, index)))
    playerRank.append(rank[index])

    index = [x for x, y in enumerate(rank) if y[0] == playerNames[1]]
    index = int("".join(map(str, index)))
    playerRank.append(rank[index])

    index = [x for x, y in enumerate(rank) if y[0] == playerNames[2]]
    index = int("".join(map(str, index)))
    playerRank.append(rank[index])

    index = [x for x, y in enumerate(rank) if y[0] == playerNames[3]]
    index = int("".join(map(str, index)))
    playerRank.append(rank[index])
    
    cursor.close()
    #closes the connection
    con.close()
    #Calls the def getPlayersInfoFromDB and passes playerNames,playerRank
    getPlayersInfoFromDB(playerNames,playerRank)

def getPlayersInfoFromDB(playerNames,playerRank):
    #create empty list called playerScore
    playerScore = []
    #try's to connect to DB and retrieve records
    try:
        #creates a MySQLConnection with the port, userName, password, host and Db name stored in it. 
        con = mysql.connector.connect(port=5004,user='root',password='password',host='localhost',database='scoreboard')
        #List of playerNames 
        records_to_select = [ (playerNames[0]) ,
                            (playerNames[1]),
                            (playerNames[2]) ,
                            (playerNames[3]) 
                            ]
        #Sql query to run                    
        sql_select_query = " SELECT Score FROM results WHERE  Player = %s ; "
        
        #passes the sql connection to myCursor  
        myCursor = con.cursor(buffered=True)
        #excutes the sql query with the players name added to it. 
        myCursor.execute(sql_select_query, (records_to_select[0], ))
        record = myCursor.fetchone()
        
        myCursor.execute(sql_select_query, (records_to_select[1], ))
        record = myCursor.fetchone()
        
        myCursor.execute(sql_select_query, (records_to_select[2], ))
        record = myCursor.fetchone()
        
        myCursor.execute(sql_select_query, (records_to_select[3], ))
        record = myCursor.fetchone()
        #print (myCursor.rowcount, "Record  successfully retrived results table")
    #if theres an error goes here and prints to console 
    except mysql.connector.Error as error :
         print("Failed Selecting record from results table {}".format(error))
    #closes myCursor
    myCursor.close()
    #closes the connection
    con.close()
    #callls writetoXML def and passes the playerRank list
    writeToXML(playerRank)

def writeToXML(playerRank):
    #sets the root element to ScoreList
    root = ET.Element("ScoreList")
    root = ET.SubElement(root,"ScoreList")
    #list of strings
    types = "UserName", "Score","Rank"
    #for loop that adds the players UserName, Score, Rank as childern of the player
    for j in range(len( playerRank)):
        usr = ET.SubElement(root,"Player")
        for i in range(3):
            info = ET.SubElement(usr,types[i])
            info.text = str(playerRank[j][i])
            #usr.append(info)
        
    #Creteas an XML element
    XMLtree = ET.ElementTree(root )    
    #Creates f as a BytesIO()
    f = BytesIO()
    #writes to f instead of a file so it can add the heading  
    XMLtree.write(f ,encoding='utf-8', xml_declaration=True )
    #xmlstr = ElementTree.tostring(et, encoding='utf8', method='xml')##Test code need to change fix TypeError: a bytes-like object is required, not 'ElementTree'
    xml_bytes = f.getvalue()
    #creates  global xmlRETURNTHIS used to store xml_bytes(Used as issue with returning a variable after going through multiple definitions)
    global  xmlRETURNTHIS
    xmlRETURNTHIS = xml_bytes
    #xmlRETURNTHIS = XMLtree

    #returnDef()


def returnDef():
    #created global as issue with returning from a def called mutiple times 
    global  xmlRETURNTHIS
    return xmlRETURNTHIS


