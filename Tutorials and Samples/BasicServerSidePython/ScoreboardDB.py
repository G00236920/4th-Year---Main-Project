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
   
    for player in tree.findall('Player'):
            score = player.get('Score')
            name = player.get('UserName')
            playerNames.append(name )
            playerScore.append (score)
    writeToDB(playerNames,playerScore)
    
def writeToDB(playerNames,playerScore):
    try:
        #MariaDB Connection
        con = mysql.connector.connect(port=5004,user='root',password='password',host='localhost',database='scoreboard')
        records_to_insert = [ (playerNames[0],playerScore[0]) ,
                            (playerNames[1],playerScore[1]),
                            (playerNames[2],playerScore[2]) ,
                            (playerNames[3],playerScore[3]) 
                            ]
        sql_insert_query = " INSERT INTO results (Player, Score) VALUES (%s,%s) ON DUPLICATE KEY UPDATE Score = VALUES(Score) + Score ; "
        myCursor = con.cursor()
        myCursor.executemany(sql_insert_query, records_to_insert)
        
        con.commit()
 
       # print (myCursor.rowcount, "Record inserted successfully into scoreboard results table")
        
    except mysql.connector.Error as error :
        print("Failed inserting record into results table {}".format(error))

    myCursor.close()
    con.close()
    rankDB(playerNames)
    
def rankDB(playerNames):
    playerRank = []
    
    con = mysql.connector.connect(port=5004,user='root',password='password',host='localhost',database='scoreboard')

    cursor = con.cursor(buffered=True)
    cursor.execute ("SELECT Player, Score ,RANK() OVER (ORDER BY Score DESC) world_rank FROM results ;")
    rank = cursor.fetchall() 

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
    con.close()
    getPlayersInfoFromDB(playerNames,playerRank)

def getPlayersInfoFromDB(playerNames,playerRank):
    playerScore = []
    try:
        con = mysql.connector.connect(port=5004,user='root',password='password',host='localhost',database='scoreboard')
        records_to_select = [ (playerNames[0]) ,
                            (playerNames[1]),
                            (playerNames[2]) ,
                            (playerNames[3]) 
                            ]
        sql_select_query = " SELECT Score FROM results WHERE  Player = %s ; "
        
        myCursor = con.cursor(buffered=True)
        myCursor.execute(sql_select_query, (records_to_select[0], ))
        record = myCursor.fetchone()
        
        myCursor.execute(sql_select_query, (records_to_select[1], ))
        record = myCursor.fetchone()
        
        myCursor.execute(sql_select_query, (records_to_select[2], ))
        record = myCursor.fetchone()
        
        myCursor.execute(sql_select_query, (records_to_select[3], ))
        record = myCursor.fetchone()
        #print (myCursor.rowcount, "Record  successfully retrived results table")
        
    except mysql.connector.Error as error :
         print("Failed Selecting record from results table {}".format(error))
    
    myCursor.close()

    con.close()
    writeToXML(playerRank)

def writeToXML(playerRank):
    usrconfig = ET.Element("data")
    usrconfig = ET.SubElement(usrconfig,"results")
    types = "Player", "Score","Rank"
    for name in range(len( playerRank)):
            
            for i in range(3):
                usr = ET.SubElement(usrconfig,types[i])
                usr.text = str(playerRank[name][i])

    XMLtree = ET.ElementTree(usrconfig )
    f = BytesIO()
    XMLtree.write(f ,encoding='utf-8', xml_declaration=True, standalone="yes" )
    xml_bytes = f.getvalue()

    global  xmlRETURNTHIS
    xmlRETURNTHIS = xml_bytes
    returnDef()
    
def returnDef():

    global  xmlRETURNTHIS
    return xmlRETURNTHIS


