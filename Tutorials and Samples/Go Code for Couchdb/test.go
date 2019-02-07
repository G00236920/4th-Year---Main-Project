package main

import (
	"log"
	"net/url"

	"github.com/zemirco/couchdb"
)

func main() {
	u, err := url.Parse("http://127.0.0.1:5984/")
	if err != nil {
		panic(err)
	}
	// create a new client
	client, err := couchdb.NewClient(u)
	if err != nil {
		panic(err)
	}
	// get some information about your CouchDB
	info, err := client.Info()
	if err != nil {
		panic(err)
	}
	log.Println(info)

}
