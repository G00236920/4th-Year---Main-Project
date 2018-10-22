package main

import (
	"fmt"
	"net"
	"os"
	"container/list"
)

const (
	CONN_HOST = ""
	CONN_PORT = "5002"
	CONN_TYPE = "tcp"
)

	
type server struct {
	username string
	ipAddress  string
}

func main() {
	// Listen for incoming connections.
	l, err := net.Listen(CONN_TYPE, CONN_HOST+":"+CONN_PORT)
	if err != nil {
		fmt.Println("Error listening:", err.Error())
		os.Exit(1)
	}
	// Close the listener when the application closes.
	defer l.Close()

	fmt.Println("Listening on " + CONN_HOST + ":" + CONN_PORT)

	for {
		// Listen for an incoming connection.
		conn, err := l.Accept()

		if err != nil {
			fmt.Println("Error accepting: ", err.Error())
			os.Exit(1)
		}

		// Handle connections in a new goroutine(eg Thread)
		go handleRequest(conn)
	}
}

// Handles incoming requests.
func handleRequest(conn net.Conn) {

	serverList := list.New()

	s := server{username: "Sean", ipAddress: "127.0.0.1"}
	serverList.PushFront(s);
	t := server{username: "John", ipAddress: "127.0.0.1"}
	serverList.PushFront(t);
	q := server{username: "paul", ipAddress: "127.0.0.1"}
	serverList.PushFront(q);

	fmt.Println(conn.RemoteAddr())

	// Close the connection when you're done with it.
	conn.Close()
}