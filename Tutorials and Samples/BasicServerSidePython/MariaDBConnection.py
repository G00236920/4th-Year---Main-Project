
import mysql.connector

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

