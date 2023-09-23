using ImagineApps.Infrastructure.UnitOfWork.Contracts;
using Microsoft.Extensions.Configuration;

namespace ImagineApps.Infrastructure.UnitOfWork.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUnitOfWorkAdapter Create(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString)) connectionString = _configuration.GetConnectionString("ImagineAppsDB");

            return new UnitOfWorkAdapter(connectionString);
        }
    }
}
