#testv123456
#test 2 
#test 3
#test 4

    
#  127.0.0.1

import mysql.connector 

conn = mysql.connector.connect(
         user='myuser',
         password='mypassword',
         host='localhost',
         database='pythontest')

conn.close()
