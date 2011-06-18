using System.Transactions;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	//public class TxHandler<T> : Consumes<T>.All where T : class
	//{
	//    private readonly Consumes<T>.All _Targ;

	//    public TxHandler(Consumes<T>.All targ)
	//    {
	//        _Targ = targ;
	//    }

	//    public void Consume(T message)
	//    {
	//        using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() {IsolationLevel = IsolationLevel.ReadCommitted}))
	//        {
	//            _Targ.Consume(message);
	//            t.Complete();
	//        }
	//    }
	//}
}