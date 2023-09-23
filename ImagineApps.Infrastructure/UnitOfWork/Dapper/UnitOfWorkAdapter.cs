using ImagineApps.Infrastructure.UnitOfWork.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace ImagineApps.Infrastructure.UnitOfWork.Dapper
{
    public class UnitOfWorkAdapter : IUnitOfWorkAdapter
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public IUnitOfWorkRepository Repositories { get; set; }

        public UnitOfWorkAdapter(string connectionString)
        {
            Initialize(connectionString);
        }

        private void Initialize(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            Repositories = new UnitOfWorkRepository(_connection, _transaction);
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Connection?.Close();
                _transaction.Connection?.Dispose();
                _transaction.Dispose();
            }

            _transaction = null;
            Repositories = null;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
}
