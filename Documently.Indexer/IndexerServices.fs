namespace Documently.Indexer
  open MassTransit
  open Documently.Domain.Events
  open CommonDomain.Persistence
  open System.IO
  open System.Text
  open System.Threading

  type IndexerService(busIn:IServiceBus, lookupPath:string) =
    let bus = busIn
    let p = lookupPath
    interface Consumes<AssociatedIndexingPending>.All with
      member x.Consume msg =
       printfn "got msg.%A" msg.BlobId
       let txt = File.ReadAllText(p, Encoding.UTF8)
       printfn "file contents:"
       printfn "%A" txt
       let ret = DocumentIndexed(msg.AggregateId)
       Thread.Sleep 200 // todo: parse the file!
       bus.Publish ret