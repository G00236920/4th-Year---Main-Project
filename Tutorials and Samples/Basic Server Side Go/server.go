package main

import (
	"bufio"
	"fmt"
	"net"
	"os"
	"log"
	"encoding/json"
	db "./db"
)

const (
	CONN_HOST = ""
	CONN_PORT1 = "5002"
	CONN_PORT2 = "5003"
	CONN_TYPE = "tcp"
)

	
type Server struct {
	Username string
	IpAddress  string
}

func main() {
	go listen(CONN_PORT2)
	listen(CONN_PORT1)
}

func listen(port string) {

	// Listen for incoming connections.
	l, err := net.Listen(CONN_TYPE, CONN_HOST+":"+port)
	if err != nil {
		fmt.Println("Error listening:", err.Error())
		os.Exit(1)
	}
	
	// Close the listener when the application closes.
	defer l.Close()

	fmt.Println("Listening on " + CONN_HOST + ":" + port)

	for {
		// Listen for an incoming connection.
		conn, err := l.Accept()

		if err != nil {
			fmt.Println("Error accepting: ", err.Error())
			os.Exit(1)
		}

		// Handle connections in a new goroutine(eg Thread)
		switch port {
		case "5002":
			go SendList(conn)
		case "5003":
			go addToList(conn)
		}

	}
}

// Handles incoming requests.
func SendList(conn net.Conn) {

	serverList := db.GetUsers()

	serverJson, err := json.Marshal(serverList)
	if err != nil {
		log.Fatal(err)
	}

	conn.Write(serverJson)

	// Close the connection when you're done with it.
	conn.Close()

}

// Handles incoming requests.
func addToList(conn net.Conn) {
	
	ip, _ , _ := net.SplitHostPort(conn.RemoteAddr().String())

	message, _ := bufio.NewReader(conn).ReadString('\n')

	fmt.Println(message)

	db.AddOne("stan", ip)

	// Close the connection when you're done with it.
	conn.Close()
	
}