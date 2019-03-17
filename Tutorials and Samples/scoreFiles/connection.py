#python 3.5  other versions can't use mysql.connector without getting error
import socketserver
import socket
import time
import datetime
 

from xml.etree import ElementTree as ET
from threading import Thread

class MyTCPHandler(socketserver.BaseRequestHandler):

    def handle(self):
        self.data = self.request.recv(1024).strip()
        print ("{} wrote:".format(self.client_address[0]))
       # print (self.data)
        ts = time.time()
        st = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S')
        print (st)
        print ("________________________________________________")
        print("Gets Here 1")

        self.request.sendall(self.data.upper())
        print("Gets Here 2")

        #newstr = oldstr.replace("M", "")
        text = self.data
        text1= text.decode('utf-8')
        #parseXML= text1.replace("b'", "")
        #parseXML = self.data[55:]
        print (text1)
        print("Gets Here 3************************************")
        text_file = open("Output.xml", "w")
        text_file.write(text1)
        text_file.close()
        text_file = open("Output.txt", "w")
        text_file.write(text1)
        text_file.close()
       # text2= str(text1)
        #ET.fromstring(self.data.encode('utf-16-be'))
       # tree = ET.fromstring(text2)


       # tree.write(open('person.xml', 'w'), encoding='UTF-16')
       # print("Gets Here 4")
       # with open("person.xml", "w") as f:
        #    f.write(ET.tostring(tree))

def listenToPort1():   
    print("Listening on 5005.......")
    server = socketserver.TCPServer((HOST1, PORT1), MyTCPHandler)
    server.serve_forever()

def listenToPort2():   
    print("Listening on 5006.......")
    server = socketserver.TCPServer((HOST2, PORT2), MyTCPHandler)
    server.serve_forever()

if __name__ == "__main__":
    HOST1, PORT1 = '0.0.0.0', 5005
    HOST2, PORT2 = '0.0.0.0', 5006

    Thread( target = listenToPort1 ).start()
    Thread( target = listenToPort2 ).start() 
