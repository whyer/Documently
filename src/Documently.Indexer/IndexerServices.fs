namespace Documently.Indexer
  open MassTransit
  open Documently.Messages.DocMetaEvents
  open Documently.Messages.Indexer
  open System.IO
  open System.Text
  open System.Threading

  type IndexerService(busIn:IServiceBus, lookupPath:string) =
    let bus = busIn
    let p = lookupPath
    interface Consumes<DocumentUploaded>.Context with
      member x.Consume ctx =
        let msg = ctx.Message
        printfn "got msg.%A" msg.Data
        let txt = File.ReadAllText(p, Encoding.UTF8)
        printfn "file contents:"
        printfn "%A" txt
        bus.Publish<IndexingCompleted>(
          { new IndexingCompleted with
              member x.DocumentId = msg.AggregateId
              member x.CorrelationId = msg.AggregateId })