#Documently

Documently is a document indexer written with CQRS as a sample project to show system architecture with CQRS.

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