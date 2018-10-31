package main

import (
    "fmt"
    "github.com/mediocregopher/radix.v2/redis"
	"log"
	"strconv"
)

var address = "localhost:6379"
var protocol = "tcp"

type User struct {
    username  string
    ipaddress string
}

func getList() []User {

	users := []User{}
	count := 0

	conn, err := redis.Dial(protocol, address)
	if err != nil {
        log.Fatal(err)
	}

	for true {

		count++
		userNo := "user:"+strconv.Itoa(count)

		name, err := conn.Cmd("HGET", userNo, "username").Str()
		if err != nil {
			return users
		}

		ip, err := conn.Cmd("HGET", userNo, "ipaddress").Str()
		if err != nil {
			log.Fatal(err)
		}

		usr := User {
			username: name,
			ipaddress: ip,
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
	
	count := getNoOfGames()+1
	userNo := "user:"+strconv.Itoa(count)

	resp := conn.Cmd("HMSET", userNo , "username", user, "ipaddress", ip)
    if resp.Err != nil {
		log.Fatal(resp.Err)
    }

    fmt.Println("Address Added!")
}

func getNoOfGames()int {

	conn, err := redis.Dial(protocol, address)
	if err != nil {
        log.Fatal(err)
    }

    defer conn.Close()

	count := 0

	for true {

		
		userNo := "user:"+strconv.Itoa(count+1)

		_, err := conn.Cmd("HGET", userNo, "username").Str()
		if err != nil {
			return count
		}
		
		count++

	}

	return count
}