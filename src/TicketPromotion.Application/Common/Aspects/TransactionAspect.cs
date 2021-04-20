using System.Transactions;
using MethodBoundaryAspect.Fody.Attributes;

namespace TicketTypePromotion.Application.Common.Aspects
{
    public sealed class TransactionAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = new TransactionScope();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var transactionScope = (TransactionScope)args.MethodExecutionTag;

            transactionScope.Complete();
            transactionScope.Dispose();
        }

        public override void OnException(MethodExecutionArgs args)
        {
            var transactionScope = (TransactionScope)args.MethodExecutionTag;

            transactionScope.Dispose();
        }
    }
}
