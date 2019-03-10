#python 3.5  other versions can't use mysql.connector without getting error
import mysql.connector
import socket
import xml.etree.cElementTree as etree
import xml.etree.ElementTree as ET
from mysql.connector.cursor import MySQLCursorPrepared
from io import StringIO

def startServer():
    # get the hostname
    #host =  socket.gethostbyname(socket.getfqdn())
    host = '127.0.0.1'
    port = 5004  # initiate port no above 1024

    server_socket = socket.socket()  # get instance
    # look closely. The bind() function takes tuple as argument
    server_socket.bind((host, port))  # bind host address and port together
    
    # configure how many client the server can listen simultaneously
    server_socket.listen(1)
    print("Listening On Host: "+ str(host) + " Port:"+  str(port) )


    conn, address = server_socket.accept()  # accept new connection
    print("Connection from: " + str(address))
    while True:
        # receive data stream. it won't accept data packet greater than 1024 bytes
        data = conn.recv(1024).decode()
        
        #writeToDB needs to to get data. So data will need to broke into playerName/playerscore
        if not data:
            # if data is not received break
            break
        print("from connected user: " + str(data))
        data = input(' -> ')
        conn.send(data.encode())  # send data to the client

    conn.close()  # close the connection

def main():
    print("main DEF***********************************")
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    host = "localhost"
    port = 5006
    addr = (host,port)
    s.connect(addr)
    print ("Got connection from Python", addr)
   
   # readXML()
    readDB()

def readXML(tree):
    print("readXML DEF***********************************")
    playerNames = []
    playerScore = []
   
    for player in tree.findall('Player'):
            score = player.get('Score')
            name = player.get('UserName')
            playerNames.append(name )
            playerScore.append (score)


    #print(playerNames ,playerScore )
    writeToDB(playerNames,playerScore)
    

def writeToDB(playerNames,playerScore):
    print("writeToDB DEF***********************************")
    
    print(playerNames ,playerScore )
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
       
        
        print (myCursor.rowcount, "Record inserted successfully into scoreboard results table")
        
    except mysql.connector.Error as error :
        print("Failed inserting record into results table {}".format(error))
    
    

    myCursor.close()

    con.close()
    rankDB(playerNames)
    getPlayersInfoFromDB(playerNames)
    #readDB()
def rankDB(playerNames):
    playerRank = []
    print("Rank DB DEF***********************************")
    
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


    
    print(playerRank)
    writeToXML(playerRank)
    
    cursor.close()
    con.close()

def getPlayersInfoFromDB(playerNames):
    print("getPlayersInfoFromDB DEF***********************************")
    playerScore = []
    print(playerNames)
    try:
        #MariaDB Connection
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
        print(record)
        
        myCursor.execute(sql_select_query, (records_to_select[1], ))
        record = myCursor.fetchone()
        print(record)
        
        myCursor.execute(sql_select_query, (records_to_select[2], ))
        record = myCursor.fetchone()
        print(record)
        
        myCursor.execute(sql_select_query, (records_to_select[3], ))
        record = myCursor.fetchone()
        print(record)

        print (myCursor.rowcount, "Record  successfully retrived results table")
        
    except mysql.connector.Error as error :
         print("Failed Selecting record from results table {}".format(error))
    
    

    myCursor.close()

    con.close()

def readDB():
    print("readDB DEF***********************************")
    playerNames = []
    playerScore = []
    con = mysql.connector.connect(port=5004,user='root',password='password',host='localhost',database='scoreboard')

    cursor = con.cursor()
    query = ("SELECT Player , Score FROM results ORDER BY Score DESC")

    cursor.execute(query)
    print ("cursor")
    print (cursor)
    print ("cursor")
    for (Player,Score) in cursor:
        playerNames.append(Player)
        playerScore.append(Score)

    res = "\n".join("{} {}".format(x, y) for x, y in zip(playerNames, playerScore))
    print(res)
    

def writeToXML(playerRank):
    print("writeToXML Method")

    print(playerRank[0][0])
    usrconfig = ET.Element("data")
    usrconfig = ET.SubElement(usrconfig,"results")
    types = "Player", "Score","Rank"
    for name in range(len( playerRank)):
            
            for i in range(3):
                usr = ET.SubElement(usrconfig,types[i])
                usr.text = str(playerRank[name][i])
                


    tree = ET.ElementTree(usrconfig)
    print(tree)
    tree.write("details.xml",encoding='utf-8', xml_declaration=True)

    


def mariaConn():
    print("mariaConn DEF***********************************")
    #MariaDB Connection
    con = mysql.connector.connect(port=5006,user='root',password='password',host='localhost',database='pythontest')

    cursor = con.cursor()
    query = ("SELECT playerName , score FROM test")

    cursor.execute(query)

    print("")
    print("Unformatted")
    print("")
    for (playerName,score) in cursor:
        print("{},{}".format(
    playerName,score))


    query1 = ("SELECT DENSE_RANK() OVER (ORDER BY score DESC) AS dense_rank, playerName, score FROM test ORDER BY score DESC;")

    cursor.execute(query1)

    print("")
    print("Sorted")
    print("WR| PlayerName  |Score")
    print("")

    for ( dense_rank,playerName,score) in cursor:
        print("{} | {}  |{}".format(
    dense_rank,playerName,score))


    cursor.close()
    con.close()


if __name__ == '__main__':
    startServer()
