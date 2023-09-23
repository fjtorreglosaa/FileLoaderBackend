namespace ImagineApps.Infrastructure.UnitOfWork.Contracts
{
    public interface IUnitOfWork
    {
        IUnitOfWorkAdapter Create(string connectionString = null);
    }
}
