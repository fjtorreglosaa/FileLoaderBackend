using Dapper;
using ImagineApps.Domain.Entities;
using ImagineApps.Infrastructure.Repositories.Contracts;
using ImagineApps.Infrastructure.Utilities;
using System.Data;
using static Dapper.SqlMapper;

namespace ImagineApps.Infrastructure.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;

        public BankRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _transaction = transaction;
            _connection = connection;
        }

        public async Task<IReadOnlyList<Bank>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Bank>(SQLBank.GetBanks);

            return result.ToList();
        }

        public async Task<Bank> GetByIdAsync(string id)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<Bank>(SQLBank.GetBankById, new { Bank_ID = id }, _transaction);

            return result ;
        }
    }
}
