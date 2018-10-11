package main

import (
	"fmt"
	"net/url"

	"github.com/zemirco/couchdb"
)

// create your own document
type hostDocument struct {
	couchdb.Document
	User  string `json:"user"`
	Ip string `json:"ip"`
}

// start
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
	fmt.Println(info)

	// create a database
	if _, err = client.Create("host"); err != nil {
		panic(err)
	}

	// use your new "host" database and create a document
	db := client.Use("host")
	doc := &hostDocument{
		Ip: "123",
		User:  "jon",
	}
	result, err := db.Post(doc)
	if err != nil {
		panic(err)
	}

	// get id and current revision.
	if err := db.Get(doc, result.ID); err != nil {
		panic(err)
	}

	// delete document
	/*if _, err = db.Delete(doc); err != nil {
		panic(err)
	}

	// and finally delete the database
	if _, err = client.Delete("dummy"); err != nil {
		panic(err)
	}*/

}