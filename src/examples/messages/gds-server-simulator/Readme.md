## GDS Server Simulator

This example serves as very basic simulator for the GDS system.

It is shipped as a `Java (maven)`  application - its dependencies are [netty (4.1.45)](https://github.com/netty/netty) and [MessagePack (0.8.15)](https://github.com/msgpack/msgpack-java) (as specified in the `pom.xml` file).
 
 Please keep in mind that the simulator will _not_ be implemented in other languages.

## How to build

You require to build and run the simulator: 

* [Oracle JDK 8](http://www.oracle.com/technetwork/java/), or newer
* [Apache Maven](http://maven.apache.org/)

## How to run

The entry point of the application is in the `hu.gds.examples.simulator.Main` class.

## What can it do?

It can receive requests and send response messages specified in the [Wiki](https://github.com/arh-eu/gds/wiki/Messages). 

## What can I use it for?
To test and tune and develop your client application (even offline) without having access to a fully working GDS system.

## What are the limits?

Since this is just a basic simulator, it has no underlying storage, SQL-parsing or PermissionConfig integration, or any of the business logic implemented, meaning the data is _static_ and dummy in the program - you cannot effectively modify it or use it as a live GDS instance.

Also, many details of your requests will be ignored - it is not the goal of this software to cover the functionality of the GDS. 

This does not mean your requests are not checked at all - packages violating the structures of the messages will be handled like invalid requests, meaning the response will contain description of these errors. 

## Which scenarios are covered then?

 - You can _connect_ to the GDS with the username `user` (any other operation without a connection message first will be declined by an appropriate response message `Error 401 - Unauthorized`). Trying to connect with an other username will result in an error as well.
 - You can send a `SELECT` request - by a [QueryRequest (10)](https://github.com/arh-eu/gds/wiki/Query-request) message. Contents of your request (the specified SQL-string) will be ignored - you will receive a response ([QueryRequestACK (11)](https://github.com/arh-eu/gds/wiki/Query-request-ACK)) with some predefined values.
 - You can continue an existing `SELECT` query by sending a [NextQueryRequest (12)](https://github.com/arh-eu/gds/wiki/Next-Query-Page-request). This will look like it is continuing an existing request from the rows it stopped last time - you will receive a response ([QueryRequestACK (11)](https://github.com/arh-eu/gds/wiki/Query-request-ACK)) as well.
