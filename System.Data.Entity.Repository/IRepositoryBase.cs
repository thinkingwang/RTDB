using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;

namespace System.Data.Entity.Repository
{
    public interface IRepositoryBase
    {
        bool Add<T>(T entity) where T : class;
        bool AddMultiple<T>(List<T> entities) where T : class;
        bool AddMultipleWithCommit<T>(List<T> entities) where T : class;
        bool AddOrUpdate<T>(T entity) where T : class;
        bool AddOrUpdateMultiple<T>(List<T> entities) where T : class;
        bool AddOrUpdateMultipleCommit<T>(List<T> entities) where T : class;

        void CommitTransaction(bool startNewTransaction = false);

        Int32 Count<T>() where T : class;

        void Dispose();

        bool Delete<T>(T entity) where T : class;
        bool DeleteMultiple<T>(List<T> entities) where T : class;
        bool DeleteMultipleWithCommit<T>(List<T> entities) where T : class;

        void Detach(object entity);
        void Detach(List<object> entities);

        IQueryable<T> Find<T>(Expression<Func<T, bool>> where) where T : class;
// ReSharper disable MethodOverloadWithOptionalParameter
        IQueryable<T> Find<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : class;
// ReSharper restore MethodOverloadWithOptionalParameter
        
        T First<T>(Expression<Func<T, bool>> where) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : class;

        IQueryable<T> GetAll<T>() where T : class;
// ReSharper disable MethodOverloadWithOptionalParameter
        IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class;
// ReSharper restore MethodOverloadWithOptionalParameter

        DbConnection GetConnection();

        void SaveChanges();
        void SetIdentityCommand();
        void SetConnectionString(string connectionString);
        void SetIsolationLevel(IsolationLevel isolationLevel);
        void SetRethrowExceptions(bool rehtrowExceptions);
        void SetTransactionType(TransactionTypes transactionType);
        void SetUseTransaction(bool useTransaction);
        
        T Single<T>(Expression<Func<T, bool>> where) where T : class;
        T SingleOrDefault<T>(Expression<Func<T, bool>> where) where T : class;
        T SingleOrDefault<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> include) where T : class;

        bool Update<T>(T entity) where T : class;
        bool UpdateMultiple<T>(List<T> entities) where T : class;
        bool UpdateMultipleWithCommit<T>(List<T> entities) where T : class;
    }
}
