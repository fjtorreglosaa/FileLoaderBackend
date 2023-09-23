using ImagineApps.Infrastructure.Repositories;
using ImagineApps.Infrastructure.Repositories.Contracts;
using ImagineApps.Infrastructure.UnitOfWork.Contracts;
using System.Data;

namespace ImagineApps.Infrastructure.UnitOfWork.Dapper
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public IBankRepository BankRepository { get; set; }

        public UnitOfWorkRepository(IDbConnection connection, IDbTransaction transaction)
        {
            BankRepository = new BankRepository(connection, transaction);
        }
    }
}
