using ImagineApps.Application.Utilities.Dtos;

namespace ImagineApps.Application.Features.FileHandler.Contracts
{
    public interface ITXTFromPathHandler
    {
        Task<(FileInformationOutputDto Result, string ErrorMessage)> GetOutputData(string path);
    }
}
