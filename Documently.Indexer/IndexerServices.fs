namespace Documently.Indexer
  open MassTransit
  open Documently.Domain.Events
  
  type IndexerService(busIn:IServiceBus) =
    let bus = busIn
    interface Consumes<AssociatedIndexingPending>.All with
      member x.Consume msg = printf "msg.%A" msg.BlobId