# Documently

This is a sample application that showcases an CQRS architecture with Event Sourcing and Domain Driven Design with a number of excellent open source software technologies. I (Henrik) want to make it easy for people to get started with this architecture - why not try to maximize developer happiness and make the world a better place?

In its current iteration, documently is not complete, but it is runnable and you can learn the basic concepts of CQRS from it. These concepts include the domain-side of things (with aggregate roots, entities and value objects), a service bus that takes care of message handling, two different client implementations (Console + WPF) and a F# indexer service that uses *Support Vector Machines* to parse and index the documents.

## Getting started

The *getting started* section assumes you are standing in the root of the solution, where you can see the 'src' directory.

 1. Start the event-store and view-store - Raven.

RavenDB is used as the eventstore and for the read side and must 
be started by going starting `External Libs\Raven\Server\Raven.Server.exe`.

There are two sample clients: one WPF Client with a nice UI and one console application for a quick start. 

Visual Studio 2010 Solution using .Net Framework 4.
Set the project Documently.WpfClient as you start-project.

It's based on samples by Mark Nijhof: https://github.com/MarkNijhof/Fohjin
Greg Young: http://github.com/gregoryyoung/m-r
http://dddsamplenet.codeplex.com/
and uses the eventstore and commondomain libraries by Jonathan Oliver: https://github.com/joliver/
The WPF Client uses Caliburn.Micro: http://caliburnmicro.codeplex.com/

The forked variant also requires RabbitMQ. Have a look at the `network` graph for a variant which doesn't 
require RabbitMQ. In the future I might remove the requirement on the message broker with an in-memory variant.

1. Download RabbitMQ-server and install, add the `sbin` path to your PATH. Remember to copy the cookie from C:\Windows\.erlang.cookie
   only ~\.erlang.cookie.
2. Run the sample as above.

CommonDomain documentation at https://github.com/haf/CommonDomain/wiki/Intro-to-Common-Domain

###TODO

* Wpf GUI for the document stuff
* Console App for the document stuff
* Read-side of the above.