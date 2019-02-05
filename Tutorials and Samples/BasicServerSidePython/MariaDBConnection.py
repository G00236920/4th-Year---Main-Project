import mysql.connector
import socket
import xml.etree.cElementTree as etree
try:
    from StringIO import StringIO
except ImportError:
    from io import StringIO

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
host = "localhost"
port = "5004"
addr = (host,port)
s.connect(addr)

def iparse(packet):
    for _, element in etree.iterparse(packet):
        print ("%s, %s" %(element.tag, element.text))
        element.clear()
    #if complete <event> node received, publish node

data = "<feeds>"
while 1:
    chunk = s.recv(1024)
    #replace the xml doc declarations as comments
    data += (chunk.replace("<?","<!--")).replace("?>","-->")
    iparse(StringIO(data))


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

