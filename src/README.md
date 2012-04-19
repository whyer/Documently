Projects Index
==============

## Inner Circle

Projects in the inner circle is what you need to look at to understand the fundamental concepts of having a domain model respond to commands by publishing events; thereafter having read models listening to these events and updating their state. You'll also find a lightweight event-sourcing framework (2-3 classes) in the domain.

### Documently.Domain

Core domain handling customers and their documents. Documents are to be indexed and customers can be created and can share documents amongst themselves.

### Documently.Domain.CommandHandlers

The step just before calling into the domain; like this:

Message Broker [C] -> CommandHandler (e.g. AssociateWithCollectionHandler) -> DocMeta -> Repository -> Message Publisher -> Message Broker [E]s.

### Documently.Domain.CommandHandlers.Tests

The unit tests covering the different usage scenarios for the domain. They are at this level, because it's preferrable to assert on 'incoming command results in these events' over asserting on 'by calling this method on my domain entity, I cause this state change readable through the public properties'.

### Documently.Domain.Service

A Windows Service host for the command handlers and domain. Compile and run `Documently.Domain.Service install` as admin on a command line to install as a Windows Service. Uses TopShelf for hosting.

### Documently.Infrastructure

Common IoC installers for the clients and command handlers and some helper classes to fiddle C# into a really nice domain-oriented language.

### Documently.Messages

Contains the interfaces sent over the wire. Commands, DomainEvents and general Event messages can be found here.

### Documently.ReadModel

When events are published, these are the DTOs that consume those events and update their own state based on what happens in the domain.

## Outer Circle

In this circle we have Sagas and their service hosts which keep track of SLAs regarding usage of the indexing service for Documents.

### Documently.Sagas

Sagas implementing SLA/business workflow revolving around temporal logic such as "after 5 minutes, if the document index digest is still unavailble to search, publish an event stating this fact to let a human know and take action".

### Documently.Sagas.Service

A TopShelf host for the Saga which allows you to install the Saga functionality as a Windows Service through running `Documently.Sagas.Service install` in an administrator console.

### Documently.Sagas.Specs

The Unit-Tests for the sagas and their logic.