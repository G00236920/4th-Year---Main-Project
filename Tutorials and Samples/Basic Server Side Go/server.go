package main

import "net"
import "fmt"
import "bufio"

// only needed below for sample processing

func main() {

	fmt.Println("Launching server...")

	// listen on all interfaces
	ln, _ := net.Listen("tcp", ":5001")

<<<<<<< HEAD
  // listen on all interfaces
  ln, _ := net.Listen("tcp", ":5001")
=======
	// accept connection on port
	conn, _ := ln.Accept()
>>>>>>> 48f970f733c9c5e75dca9cc82de59342623af259

	// run loop forever (or until ctrl-c)
	for {
		// will listen for message to process ending in newline (\n)
		message, _ := bufio.NewReader(conn).ReadString('\n')
		// output message received
		if len(string(message)) > 0 {

<<<<<<< HEAD
  fmt.Print("Client Connected:")

  // run loop forever (or until ctrl-c)
  for {
    // will listen for message to process ending in newline (\n)
    message, _ := bufio.NewReader(conn).ReadString('\n')
    // output message received
    fmt.Print("Message Received:", string(message))
    // sample process for string received
    newmessage := strings.ToUpper(message)
    // send new string back to client
    conn.Write([]byte(newmessage + "\n"))
  }
=======
			fmt.Print("Message Received:", string(message))
		}
>>>>>>> 48f970f733c9c5e75dca9cc82de59342623af259

	}
}
