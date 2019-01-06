using System.Transactions;

namespace UserAdvice.Utilities
{
    internal static class Transaction
    {
        public static TransactionScope BeginTransaction()
        {
            return new TransactionScope(TransactionScopeOption.Required, 
                new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead },
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
