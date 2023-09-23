using ImagineApps.Infrastructure.Repositories.Contracts;

namespace ImagineApps.Infrastructure.UnitOfWork.Contracts
{
    public interface IUnitOfWorkRepository
    {
        IBankRepository BankRepository { get; }
    }
}
