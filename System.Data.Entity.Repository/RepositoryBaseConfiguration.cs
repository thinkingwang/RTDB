namespace System.Data.Entity.Repository
{
    public class RepositoryBaseConfiguration
    {
        public TransactionTypes TransactionType = TransactionTypes.DbTransaction;
        public IsolationLevel IsolationLevel = IsolationLevel.ReadUncommitted;

        public bool ProxyCreationEnabled = false;
        public bool RethrowExceptions = false;
        public bool UseTransaction = true;
        public bool LazyConnection = false;

        public int CommandTimeout = 300;

        public string ConnectionString = string.Empty;
    }
}
