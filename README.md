# Documently

Documently is a single *bounded context* with a single *domain model* and multiple *autonymous services*. The SAAS that is Documently is exposed by sending command to the `Documently.Domain.Service` and listening to corresponding events domain events.

A higher-level description of the architecture is available at the [Jayway Architecture site](http://architecture.jayway.com).

## Getting started

First, start the event-store and view-store - RavenDB.Server, in this case. It can be found in `src\packages\RavenDB.1.x.x\server`. Once this is done, install [RabbitMQ](http://www.rabbitmq.com/download.html)

There are two sample clients: one WPF Client with a nice UI and one console application for a quick start. Before using them, you need to compile the solution and start the `Documently.Domain.Service` console application. This is the domain service that handles the domain logic and broadcasts the (domain) events.

Once both RavenDB and the *Domain Service* are started without error messages, start one of the clients, or both and try it out!

**Note:** When starting *Domain Service* the first time you will need to run it as admin to avoid getting error messages from MassTransit's Performance Counters - or you can run it and just ignore those messages. 

## TODO for v2

 * StateBox-like for union and ordinary event stream merging when eventually consistent **event store** (different from the often spoken about eventual consistency of read models) - this would allow you to use Riak and other master-master type events stores and also handle for example event-stores in different countries/data centers in which case net-splits between stores may occurr.
 * Document upload in user interface
 * Document listing in user interface
 * Document details in user interface
 * Document search in user interface
 * <del>All messages interfaces + proper infrastructure</del>
 * <del>Do away with complex unit testing procedures</del>
 * <del>Better cohesion in code base by avoiding inheritance and using value objects to a greater extent and having infrastructure that is only used with command handlers in that specific assembly (that may take a couple of dependencies on infrastructure)</del>
 * <del>Better naming of commands and events based on experience with too-long-names</del>
 * <del>Better documentation for each folder</del>
 * <del>Using dynamic instead of CommonDomain</del>
 * Proper Saga example with Automatonymous and MassTransit (and EventStore?)
 * Properly handling de-duplication of events on the read-side
 * Properly handling replaying of the event store from the last received point forward on the read-side
 * Finishing implementing the Document-part of the App and WpfClient
 * Running a WPF, a iOS phone and console application side-by-side and having the system work as a whole.
 * Implement the indexing logic for the Document-part of the domain using SVMs.
 * Bump up the domain model complexity a notch to warrant DDD in the first place!

Some of the concepts you can learn include:

 * Creating a Caliburn Micro WPF client with a ribbon, which uses the *EventAggregator* pattern for its reactive UI and wires itself up with Castle Windsor.
 * How to configure MassTransit with RabbitMQ
 * How to write unit-tests for aggregate roots based on simple invoke-method, read resulting events.
 * How to use CommonDomain to publish committed events
 * Getting started with RavenDB
 * Getting started with RabbitMQ
 * Doing application validation logic (non-emptyness, etc) with *FluentValidation*
 * How to use TopShelf with the DomainService (in daemon/Windows Service mode, not hosted).

## Credits

The sample is based on code by:

 * **Greg Young**: <http://github.com/gregoryyoung/m-r>
 * and uses **[Jonathan Oliver](https://github.com/joliver/)**'s [EventStore](https://github.com/joliver/EventStore) libraries.
 * The WPF Client uses Caliburn.Micro: <http://caliburnmicro.codeplex.com/>
 * The wiki is in parts based on a presentation 2011-07-23 by Greg Young

You can find more information about CQRS in the wiki: <https://github.com/haf/Documently/wiki>