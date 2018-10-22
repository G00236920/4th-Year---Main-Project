package db

import (
    "fmt"
	"github.com/mediocregopher/radix.v2/redis"
	"log"
)

var address = "localhost:6379"
var protocol = "tcp"

type User struct {
    Username  string
    Ipaddress string
}

func GetUsers() []User {

	users := []User{}

	conn, err := redis.Dial(protocol, address)
	if err != nil {
        log.Fatal(err)
	}

	defer conn.Close()

	keys, err := conn.Cmd("keys","*").List()
	if err!=nil{
		log.Fatal(err)
	}

	for _, key := range keys {

		ipAdd, err := conn.Cmd("HGET", key, "ipaddress").Str()
		if err != nil {
			log.Fatal(err)
		}

		usr := User {
			Username: key,
			Ipaddress: ipAdd,
		}

		users = append(users, usr)
	}

	return users

}

func addOne(user string, ip string){

	conn, err := redis.Dial(protocol, address)
	if err != nil {
        log.Fatal(err)
    }

	defer conn.Close()

    resp := conn.Cmd("HMSET", user, "ipaddress", ip)
    // Check the Err field of the *Resp object for any errors.
    if resp.Err != nil {
        log.Fatal(resp.Err)
    }

    fmt.Println("Address Added!")
}