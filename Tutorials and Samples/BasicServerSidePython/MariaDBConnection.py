#python 3.5  other versions can't use mysql.connector without getting error
import mysql.connector
import socket
import xml.etree.cElementTree as etree

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
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    host = "localhost"
    port = 5006
    addr = (host,port)
    s.connect(addr)
    print ("Got connection from Python", addr)
   
   # readXML()
    readDB()

def readXML():
    from xml.dom import minidom

    playerNames = []
    playerScore = []
   
    # parse an xml file by name
    mydoc = minidom.parse('items.xml')

    results = mydoc.getElementsByTagName('player')


    # all item attributes
    print('\nAll Player names:')  
    for elem in results:  
        print(elem.attributes['name'].value)
        playerNames.append(elem.attributes['name'].value)
       

    # all Players results 
    print('\nAll Players results:')  
    for elem in results:  
        print(elem.firstChild.data)
        playerScore.append(elem.firstChild.data)
        

    print(playerNames ,playerScore )
    writeToDB(playerNames,playerScore)

def writeToDB(playerNames,playerScore):
    from mysql.connector.cursor import MySQLCursorPrepared
    print(playerNames ,playerScore )
    try:
        #MariaDB Connection
        con = mysql.connector.connect(port=5006,user='root',password='password',host='localhost',database='pythontest')
        
        records_to_insert = [ (playerNames[0],playerScore[0]) ,
                            (playerNames[1],playerScore[1])
                            ]
        sql_insert_query = " INSERT INTO test (playerName, score) VALUES (%s,%s) "
        myCursor = con.cursor()
        print("Error Checking 1")
        #used executemany to insert 3 rows
        myCursor.executemany(sql_insert_query, records_to_insert)
        print("Error Checking 2")
        con.commit()
        
        print (myCursor.rowcount, "Record inserted successfully into python_users table")
    except mysql.connector.Error as error :
        print("Failed inserting record into python_users table {}".format(error))
    myCursor.close()
    con.close()

def readDB():
    playerNames = []
    playerScore = []
    con = mysql.connector.connect(port=5006,user='root',password='password',host='localhost',database='pythontest')

    cursor = con.cursor()
    query = ("SELECT playerName , score FROM test")

    cursor.execute(query)

    for (playerName,score) in cursor:
        playerNames.append(playerName)
        playerScore.append(score)
    print(playerNames ,playerScore )
    writeToXML(playerNames ,playerScore )

def writeToXML(playerNames ,playerScore ):
    import xml.etree.ElementTree as ET


    usrconfig = ET.Element("data")
    usrconfig = ET.SubElement(usrconfig,"results")
    for name in range(len( playerNames)):
            usr = ET.SubElement(usrconfig,"name")
            usr.text = str(playerNames[name])
    tree = ET.ElementTree(usrconfig)
    tree.write("details.xml",encoding='utf-8', xml_declaration=True)

    


def mariaConn():
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
