module Documently.Indexer.Program

open MassTransit
open System

  [<EntryPoint>]
  let main(args : string[]) =
    let bus = ServiceBusFactory.New (fun c -> 
      RabbitMqServiceBusExtensions.UseRabbitMq c |> ignore
      c.UseRabbitMqRouting() |> ignore
      c.ReceiveFrom("rabbitmq://localhost/Documently.Indexer") |> ignore
      )
    let s = IndexerService(bus, "../../../example-documents")
    bus.SubscribeInstance(s) |> ignore
    printf "Press a key to exit..."
    Console.ReadKey true |> ignore
    bus.Dispose()
    0