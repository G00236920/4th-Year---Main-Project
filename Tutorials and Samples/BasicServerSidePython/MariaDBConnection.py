
import mysql.connector

con = mysql.connector.connect(port=5006,user='root',password='password',host='localhost',database='pythontest')

cursor = con.cursor()
query = ("SELECT id FROM test")

cursor.execute(query)


for (id) in cursor:
      print("{}".format(
   id))

cursor.close()
con.close()

