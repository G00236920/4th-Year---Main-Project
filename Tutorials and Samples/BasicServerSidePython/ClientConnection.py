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
        self.data = self.request.recv(1024).strip()
        print ("{} wrote:".format(self.client_address[0]))
        currentTime()
        testReturn = self.data 
        xmlBytes = self.data.decode("utf-8")
        xmlString = str( xmlBytes )
        
        tree = ET.fromstring(xmlString)
        
        # returnXML =  property(Maria.readXML(tree))
        Maria.readXML(tree)
        returnXML = Maria.returnDef()

        #x = bytearray(returnXML)

        self.request.sendall(returnXML)
        #self.request.sendall(x)

        print(returnXML)

        print("returnXML^^^^")

       
        print("Returned")
        '''
        try:
            shutdown(SHUT_RDWR)
            close()
        except Exception:
            pass

        
         text_file = open("Output.xml", "w")
         text_file.write(xmlString)
         text_file.close()
        
        
        returnPort = 5555 
        command = " Results Temp "
        b = command.encode('utf-8')
        print(b)
        print(command)
         
        print("over command")
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        print("Connecting to ip" , self.client_address[0])
        print("Connecting to port" , returnPort)
        s.connect((self.client_address[0], returnPort))

        print("over s.connect")
        s.send(command)
        print("over s.send")
        #print ("gets here")
        #time.sleep(2)
        resp = s.recv(1024)

        print (resp)

        print("Failed  Returning Data")
        '''


def currentTime():
    ts = time.time()
    st = datetime.datetime.fromtimestamp(ts).strftime('%Y-%m-%d %H:%M:%S')
    print (st)
    print ("________________________________________________")

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
