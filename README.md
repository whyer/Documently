# Documently

This is a sample application showcasing a DDD/CQRS architecture with Event Sourcing 
with a number of excellent open source software technologies. 
I (Henrik) want to make it easy for people to get started with this 
architecture - why not try to maximize developer happiness and make the 
world a better place and learn it yourself while you're at it?

Some of the concepts you can learn include:

 * Creating a Caliburn Micro WPF client with a ribbon, which uses the *EventAggregator* pattern for its reactive UI and wires itself up with Castle Windsor.
 * How to configure MassTransit with RabbitMQ
 * How to write unit-tests for aggregate roots based on simple invoke-method, read resulting events.
 * How to use CommonDomain to publish committed events
 * Getting started with RavenDB
 * Getting started with RabbitMQ
 * Doing application validation logic (non-emptyness, etc) with *FluentValidation*
 * How to use TopShelf with the DomainService (in daemon/Windows Service mode, not hosted).

Currently incomplete but upcoming features:

 * Properly handling de-duplication of events on the read-side
 * Properly handling replaying of the event store from the last received point forward on the read-side
 * Finishing implementing the Document-part of the App and WpfClient
 * Running a WPF, a iOS phone and console application side-by-side and having the system work as a whole.
 * Implement the indexing logic for the Document-part of the domain using SVMs.

The above tasks are rather easy to accomplish, they just take a few hours worth of time - so if you feel up for learning the concepts of CQRS and Event Sourcing, feel free to have a go and send a pull-request!

## Getting started

First, start the event-store and view-store - RavenDB.Server, in this case. It can be found in `src\packages\RavenDB.1.x.x\server`. Once this is done, install [RabbitMQ](http://www.rabbitmq.com/download.html)

There are two sample clients: one WPF Client with a nice UI and one console application for a quick start. Before using them, you need to compile the solution and start the `Documently.Domain.Service` console application. This is the domain service that handles the domain logic and broadcasts the (domain) events.

Once both RavenDB and the *Domain Service* are started without error messages, start one of the clients, or both and try it out!

**Note:** When starting *Domain Service* the first time you will need to run it as admin to avoid getting error messages from MassTransit's Performance Counters - or you can run it and just ignore those messages. 

## Credits

The sample is based on code by:

 * **Mark Nijhof**: <https://github.com/MarkNijhof/Fohjin>
 * **Greg Young**: <http://github.com/gregoryyoung/m-r>
 * <http://dddsamplenet.codeplex.com/>
 * and uses **[Jonathan Oliver](https://github.com/joliver/)**'s [EventStore](https://github.com/joliver/EventStore)  and [CommonDomain](https://github.com/haf/CommonDomain) [wiki](https://github.com/haf/CommonDomain/wiki) libraries.
 * The WPF Client uses Caliburn.Micro: <http://caliburnmicro.codeplex.com/>
 * The wiki is in parts based on a presentation 2011-07-23 by Greg Young

CommonDomain documentation at <https://github.com/haf/CommonDomain/wiki/Intro-to-Common-Domain>
You can find more information about CQRS in the wiki: <https://github.com/haf/Documently/wiki>