using ImagineApps.Application.Utilities.Dtos;

namespace ImagineApps.Application.Features.FileHandler.Contracts
{
    public interface ITXTFromStreamHandler
    {
        Task<(FileInformationOutputDto Result, string ErrorMessage)> GetOutputData(StreamReader reader);
    }
}
