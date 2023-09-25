using ImagineApps.Application.Features.FileHandler.Contracts;
using ImagineApps.Application.Utilities;
using ImagineApps.Application.Utilities.Dtos;
using ImagineApps.Infrastructure.ExternalResources.Contracts;
using MediatR;

namespace ImagineApps.Application.Features.FileHandler.Commands.TXTFromPath
{
    public class TXTRequestFromPathHandler : TXTHandler, IRequestHandler<TXTFromPathRequest, string>, ITXTFromPathHandler
    {
        public TXTRequestFromPathHandler(IMediator mediator, ITXT txt) : base(mediator, txt)
        {
        }

        public async Task<string> Handle(TXTFromPathRequest request, CancellationToken cancellationToken)
        {
            if (!request.filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)) return "The file extension is not valid, extension must be .txt";

            var data = await GetOutputData(request.filePath);

            var result = OutputResult(new FinalOutputDataDto
            {
                Result = data.Result,
                ErrorMessage= data.ErrorMessage
            });

            return result;
        }

        public async Task<(FileInformationOutputDto Result, string ErrorMessage)> GetOutputData(string path)
        {
            var fileData = await _TXT.ReadFile(path);

            var result = await GetOutputs(new BaseOutputDataDto
            {
                Data = fileData.Data,
                ErrorMessage = fileData.ErrorMessage,
            });

            return result;
        }
    }
}
