namespace ImagineApps.Infrastructure.ExternalResources.Contracts
{
    public interface IExternalFile
    {
        Task<(List<string> Data, string ErrorMessage)> ReadFile(string path);
    }
}
