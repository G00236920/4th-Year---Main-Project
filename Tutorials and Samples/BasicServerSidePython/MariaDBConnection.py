#python 3.5  other versions can't use mysql.connector without getting error
import mysql.connector
import socket
import xml.etree.cElementTree as etree

from io import StringIO
def main():
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    host = "localhost"
    port = 5006
    addr = (host,port)
    s.connect(addr)
    print ("Got connection from Python", addr)
    readXML()

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
    main()
