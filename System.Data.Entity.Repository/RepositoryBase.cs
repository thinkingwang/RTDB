using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Transactions;

namespace System.Data.Entity.Repository
{
    public class RepositoryBase<TR> : IRepositoryBase, IDisposable
        where TR : DbContext
    {
        private DbContext _model;

        private TransactionTypes _transactionType = TransactionTypes.DbTransaction;
        private TransactionScope _transactionScope;
        private IsolationLevel _isolationLevel = IsolationLevel.ReadUncommitted;
        private DbTransaction _transaction;
        private DbConnection _connection;
        
        private readonly bool _proxyCreationEnabled;
        private bool _rethrowExceptions = true;
        private bool _useTransaction = true;

        private string _connectionString = string.Empty;

        public event RepositoryBaseExceptionHandler RepositoryBaseExceptionRaised;
        public delegate void RepositoryBaseExceptionHandler(Exception exception);

// ReSharper disable StaticFieldInGenericType
        private static bool _staticLazyConnectionOverrideUsed;
        private static bool _lazyConnection;

// ReSharper restore StaticFieldInGenericType

// ReSharper disable NotAccessedField.Local
        private int _commandTimeout = 300;
// ReSharper restore NotAccessedField.Local


        internal void InitializeRepository()
        {
            if (_model == null)
            {
                var instance = (DbContext)Activator.CreateInstance(typeof(TR));
                ((IObjectContextAdapter)instance).ObjectContext.CommandTimeout = 300;
                _model = instance;

                if (_lazyConnection == false)
                    InitializeConnection();

                _model.Configuration.ProxyCreationEnabled = _proxyCreationEnabled;
            }
            else
            {
                _model.Configuration.LazyLoadingEnabled = false;
            }
        }

        internal void InitializeConnection()
        {
            if (_model != null)
            {
                if (!string.IsNullOrEmpty(_connectionString))
                {
                    _model.Database.Connection.ConnectionString = _connectionString;
                }

                _connection = ((IObjectContextAdapter)_model).ObjectContext.Connection;
                _connection.Open();
            }
        }

        public RepositoryBase()
        {
            InitializeRepository();
        }

        public RepositoryBase(bool throwExceptions, string connectionString = "", bool lazyConnection = false, TransactionTypes transactionType = TransactionTypes.DbTransaction, IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted,
            bool useTransactions = true, bool proxyCreationEnabled = false, int commandTimeout = 300)
        {
            _rethrowExceptions = throwExceptions;
            _useTransaction = useTransactions;
            _proxyCreationEnabled = proxyCreationEnabled;
            _commandTimeout = commandTimeout;
            _isolationLevel = IsolationLevel.ReadUncommitted;
            _connectionString = connectionString;
            _transactionType = transactionType;
            _isolationLevel = isolationLevel;

            if (_staticLazyConnectionOverrideUsed == false)
                _lazyConnection = lazyConnection;

            InitializeRepository();
        }

        public RepositoryBase(RepositoryBaseConfiguration configuration)
        {
            _rethrowExceptions = configuration.RethrowExceptions;
            _useTransaction = configuration.UseTransaction;
            _proxyCreationEnabled = configuration.ProxyCreationEnabled;
            _commandTimeout = configuration.CommandTimeout;
            _isolationLevel = configuration.IsolationLevel;
            _connectionString = configuration.ConnectionString;

            if (_staticLazyConnectionOverrideUsed == false)
                _lazyConnection = configuration.LazyConnection;

            InitializeRepository();
        }

        public bool Add<T>(T entity) where T : class
        {
            bool result = false;

            ProcessTransactionableMethod(() =>
            {
                try
                {
                    SetEntity<T>()
                        .Add(entity);

                    SaveChanges();

                    result = true;
                }
                catch (Exception error)
                {
                    var entry = _model.Entry(entity);
                    entry.State = EntityState.Unchanged;

                    RollBack();
                    Detach(entity);

                    if (_rethrowExceptions)
                    {
                        throw;
                    }
                    if (RepositoryBaseExceptionRaised != null)
                    {
                        RepositoryBaseExceptionRaised(error);
                    }
                }
            });
            
            return result;
        }

        public bool AddMultiple<T>(List<T> entities) where T : class
        {
            bool result = false;

            entities.ForEach(e => result = Add(e));

            return result;
        }

        public bool AddMultipleWithCommit<T>(List<T> entities) where T : class
        {
            bool result = false;

            entities.ForEach(e =>
            {
                result = Add(e);
                CommitTransaction(startNewTransaction: true);
            });

            return result;
        }

        public bool AddOrUpdate<T>(T entity) where T : class
        {
            var result = false;

            ProcessTransactionableMethod(() =>
            {
                try
                {
                    var entry = SetEntry(entity);

                    if (entry != null)
                    {
                        entry.State = entry.State == EntityState.Detached ? EntityState.Added : EntityState.Modified;
                    }
                    else
                    {
                        _model.Set<T>().Attach(entity);
                    }

                    SaveChanges();

                    result = true;
                }
                catch (Exception)
                {
                    var entry = _model.Entry(entity);
                    entry.State = EntityState.Unchanged;

                    RollBack();
                    Detach(entity);
                }
            });

            return result;
        }

        public bool AddOrUpdateMultiple<T>(List<T> entities) where T : class
        {
            var result = false;

            entities.ForEach(e => result = AddOrUpdate(e));

            return result;
        }

        public bool AddOrUpdateMultipleCommit<T>(List<T> entities) where T : class
        {
            bool result = false;

            entities.ForEach(e => {
                result = AddOrUpdate(e);
                CommitTransaction(true);
            });

            return result;
        }

        public void CommitTransaction(bool startNewTransaction = false)
        {
            if (_useTransaction)
            {
                switch (_transactionType)
                {
                    case TransactionTypes.DbTransaction:
                        if (_transaction != null && _transaction.Connection != null)
                        {
                            SaveChanges();
                            _transaction.Commit();
                        }
                        break;

                    case TransactionTypes.TransactionScope:
                        try
                        {
                            if(_transactionScope != null)
                                _transactionScope.Complete();
                        }
                        catch (Exception error)
                        {
                            if (_rethrowExceptions)
                            {
                                throw;
                            }
                            if (RepositoryBaseExceptionRaised != null)
                            {
                                RepositoryBaseExceptionRaised(error);
                            }
                        }

                        break;
                }

                if (startNewTransaction)
                    StartTransaction();
            }
            else
            {
                SaveChanges();
            }
        }

        public Int32 Count<T>() where T : class
        {
            return _model.Set<T>()
                .Count();
        }

        public bool Delete<T>(T entity) where T : class
        {
            bool result = false;

            ProcessTransactionableMethod(() =>
            {
                try
                {
                    var entry = SetEntry(entity);

                    if (entry != null)
                    {
                        entry.State = EntityState.Deleted;
                    }
                    else
                    {
                        _model.Set<T>().Attach(entity);
                    }

                    _model.Set<T>().Remove(entity);

                    result = true;
                }
                catch (Exception error)
                {
                    var entry = _model.Entry(entity);
                    entry.State = EntityState.Unchanged;

                    RollBack();
                    Detach(entity);

                    if (_rethrowExceptions)
                    {
                        throw;
                    }
                    if (RepositoryBaseExceptionRaised != null)
                    {
                        RepositoryBaseExceptionRaised(error);
                    }
                }
            });

            return result;
        }

        public bool DeleteMultiple<T>(List<T> entities) where T : class
        {
            bool result = false;

            entities.ForEach(e => result = Delete(e));

            return result;
        }

        public bool DeleteMultipleWithCommit<T>(List<T> entities) where T : class
        {
            bool result = false;

            entities.ForEach(e =>
            {
                result = Delete(e);
                CommitTransaction(startNewTransaction: true);
            });

            return result;
        }

        public void Detach(object entity)
        {
            var objectContext = ((IObjectContextAdapter)_model).ObjectContext;
            var entry = _model.Entry(entity);

            if (entry.State != EntityState.Detached)
                objectContext.Detach(entity);
        }

        public void Detach(List<object> entities)
        {
            entities.ForEach(Detach);
        }

        public IQueryable<T> Find<T>(Expression<Func<T, bool>> where) where T : class
        {
            IQueryable<T> entities = default(IQueryable<T>);

            ProcessTransactionableMethod(() => {
                entities = SetEntities<T>().Where(where);
            });

            return entities;
        }

// ReSharper disable MethodOverloadWithOptionalParameter
        public IQueryable<T> Find<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : class
// ReSharper restore MethodOverloadWithOptionalParameter
        {
            IQueryable<T> entities = SetEntities<T>();

            ProcessTransactionableMethod(() => 
            { 
                if (includes != null)
                {
                    entities = ApplyIncludesToQuery(entities, includes);
                }

                entities = entities.Where(where);
            });

            return entities;
        }

        public T First<T>(Expression<Func<T, bool>> where) where T : class
        {
            IQueryable<T> entities = SetEntities<T>();

            var entity = default(T);

            ProcessTransactionableMethod(() =>
            {
                entity =  entities
                    .First(where);
            });

            return entity;
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> entities = SetEntities<T>();

            var entity = default(T);

            ProcessTransactionableMethod(() =>
            {
                if (where != null)
                    entities = entities.Where(where);

                if (includes != null)
                {
                    entities = ApplyIncludesToQuery(entities, includes);
                }

                entity = entities.FirstOrDefault();
            });

            return entity;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            IQueryable<T> entities = default(IQueryable<T>);

            ProcessTransactionableMethod(() =>
            {
                entities = SetEntities<T>();
            });

            return entities;
        }

// ReSharper disable MethodOverloadWithOptionalParameter
        public IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class
// ReSharper restore MethodOverloadWithOptionalParameter
        {
            IQueryable<T> entities = SetEntities<T>();

            if (includes != null)
            {
                entities = ApplyIncludesToQuery(entities, includes);
            }

            return entities;
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public void SaveChanges()
        {
            _model.SaveChanges();
        }

        public void SetIdentityCommand()
        {
            var container =
                   ((IObjectContextAdapter)_model).ObjectContext.MetadataWorkspace
                      .GetEntityContainer(
                            ((IObjectContextAdapter)_model).ObjectContext.DefaultContainerName,
                            DataSpace.CSpace);

            List<EntitySetBase> sets = container.BaseEntitySets.ToList();

            foreach (EntitySetBase set in sets)
            {
                string command = string.Format("SET IDENTITY_INSERT {0} {1}", set.Name, "ON");
                ((IObjectContextAdapter)_model).ObjectContext.ExecuteStoreCommand(command);
            }
        }

        public void SetConnectionString(string connectionString)
        {
            if (_lazyConnection)
            {
                _connectionString = connectionString;
                InitializeConnection();
            }
        }

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        public void SetRethrowExceptions(bool rehtrowExceptions)
        {
            _rethrowExceptions = rehtrowExceptions;
        }

        public void SetTransactionType(TransactionTypes transactionType)
        {
            _transactionType = transactionType;
        }

        public void SetUseTransaction(bool useTransaction)
        {
            _useTransaction = useTransaction;
        }

        public T Single<T>(Expression<Func<T, bool>> where) where T : class
        {
            IQueryable<T> entities = SetEntities<T>();

            T entity = default(T);

            ProcessTransactionableMethod(() =>
            {
                entity = entities
                    .Single(where);
            });

            return entity;
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> where) where T : class
        {
            IQueryable<T> entities = SetEntities<T>();

            T entity = default(T);

            ProcessTransactionableMethod(() =>
            {
                entity = entities
                    .SingleOrDefault(where);
            });

            return entity;
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> include) where T : class
        {
            IQueryable<T> entities = SetEntities<T>();

            T entity = default(T);

            ProcessTransactionableMethod(() =>
            {
                entity = entities
                    .Include(include)
                    .SingleOrDefault(where);
            });

            return entity;
        }

        public bool Update<T>(T entity) where T : class
        {
            bool result = false;

            ProcessTransactionableMethod(() =>
            {
                try
                {
                    var entry = SetEntry(entity);

                    if (entry != null)
                    {
                        entry.State = EntityState.Modified;
                    }
                    else
                    {
                        _model.Set<T>().Attach(entity);
                    }

                    SaveChanges();

                    result = true;
                }
                catch (Exception error)
                {
                    var entry = _model.Entry(entity);
                    entry.State = EntityState.Unchanged;

                    RollBack();
                    Detach(entity);

                    if (_rethrowExceptions)
                    {
                        throw;
                    }
                    if (RepositoryBaseExceptionRaised != null)
                    {
                        RepositoryBaseExceptionRaised(error);
                    }
                }
            });

            return result;
        }

        public bool UpdateMultiple<T>(List<T> entities) where T : class
        {
            bool result = false;

            entities.ForEach(e => result = Update(e));

            return result;
        }

        public bool UpdateMultipleWithCommit<T>(List<T> entities) where T : class
        {
            bool result = false;

            entities.ForEach(e => 
            {
                result = Update(e);
                CommitTransaction(startNewTransaction: true);
            });

            return result;
        }

        public static void UseLazyConnection(bool useLazyConnection)
        {
            _lazyConnection = useLazyConnection;
            _staticLazyConnectionOverrideUsed = true;
        }

        internal IQueryable<T> ApplyIncludesToQuery<T>(IQueryable<T> entities, Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
                entities = includes.Aggregate(entities, (current, include) => current.Include(include));

            return entities;
        }

        internal void ProcessTransactionableMethod(Action action)
        {
            StartTransaction();
            action();
        }

        internal IQueryable<T> SetEntities<T>() where T : class
        {
            IQueryable<T> entities = _model.Set<T>();

            return entities;
        }

        internal DbSet<T> SetEntity<T>() where T : class
        {
            DbSet<T> entity = _model.Set<T>();

            return entity;
        }

        internal DbEntityEntry SetEntry<T>(T entity) where T : class
        {
            DbEntityEntry entry = _model.Entry(entity);

            return entry;
        }

        internal IQueryable<TR> GetQuery(Expression<Func<TR, object>> include)
        {
            IQueryable<TR> entities = SetEntities<TR>()
                .Include(include);

            return entities;
        }

        internal void RollBack()
        {
            if (_useTransaction)
            {
                if (_transactionType == TransactionTypes.DbTransaction)
                {
                    if (_transaction != null && _transaction.Connection != null)
                    {
                        _transaction.Rollback();
                    }
                }
            }
        }

        internal void StartTransaction()
        {
            if (_useTransaction)
            {
                switch(_transactionType)
                {
                    case TransactionTypes.DbTransaction:
                        if (_transaction == null || _transaction.Connection == null)
                            _transaction = _connection.BeginTransaction(_isolationLevel);
                        break;

                    case TransactionTypes.TransactionScope:
                        _transactionScope = new TransactionScope();
                        break;
                }
            }
        }

        public void Dispose()
        {
            CommitTransaction();

            if (!Equals(_model, null))
            {
                _model.Dispose();
                _model = null;
            }

            if (_connection != null)
            {
                _connection.Close();
            }

            _transaction = null;
            _transactionScope = null;
            _connection = null;
        }
    }
}
