using ImagineApps.Application.Utilities.Dtos;

namespace ImagineApps.Application.Features.FileHandler.Contracts
{
    public interface ITXTHandler
    {
        Task<(FileInformationOutputDto Result, string ErrorMessage)> GetOutputs(BaseOutputDataDto output);
        string OutputResult(FinalOutputDataDto data);
    }
}
