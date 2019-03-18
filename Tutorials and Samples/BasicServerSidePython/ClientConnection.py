#python 3.5  other versions can't use mysql.connector without getting error
import socketserver
import socket
from socket import SHUT_RDWR
import time
import datetime
import xml.dom.minidom as Dom
import MariaDBConnection as Maria

from xml.etree import ElementTree as ET
from threading import Thread


class MyTCPHandler(socketserver.BaseRequestHandler):

    def handle(self):
        self.data = self.request.recv(1024).strip()# recieves data from c# program
        print ("{} wrote:".format(self.client_address[0]))# prints ip recieved from 
        currentTime() # gets current time 
        xmlBytes = self.data.decode("utf-8")# decodes bytes
        xmlString = str( xmlBytes )#coverts bytes to string 
        tree = ET.fromstring(xmlString)#creates xml from xmlString
        
        Maria.readXML(tree)#calls readXml def in MariaDBConnection.py and passes tree into it 
        returnedXML = Maria.returnDef()#calls returnDef def in MariaDBConnection.py and stores return in returnedXML

        self.request.sendall(returnedXML)#returns xml to c# program 

def currentTime():#sets and prints current time 
    ts = time.time()
    st = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S')
    print (st)
    print ("________________________________________________")

def listenToPort1():#sets up thread to listen on localhost & port 5005 and listen forever   
    print("Listening on 5005.......")
    server = socketserver.TCPServer((HOST1, PORT1), MyTCPHandler)
    server.serve_forever()

def listenToPort2():#sets up thread to listen on localhost & port 5006 and listen forever    
    print("Listening on 5006.......")
    server = socketserver.TCPServer((HOST2, PORT2), MyTCPHandler)
    server.serve_forever()

if __name__ == "__main__":
    HOST1, PORT1 = '0.0.0.0', 5005
    HOST2, PORT2 = '0.0.0.0', 5006

    Thread( target = listenToPort1 ).start()
    Thread( target = listenToPort2 ).start() 
