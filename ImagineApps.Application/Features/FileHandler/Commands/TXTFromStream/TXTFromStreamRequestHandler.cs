using ImagineApps.Application.Features.FileHandler.Contracts;
using ImagineApps.Application.Utilities.Dtos;
using ImagineApps.Infrastructure.ExternalResources.Contracts;
using MediatR;

namespace ImagineApps.Application.Features.FileHandler.Commands.TXTFromStream
{
    public class TXTFromStreamRequestHandler : TXTHandler, IRequestHandler<TXTFromStreamRequest, string>, ITXTFromStreamHandler
    {
        public TXTFromStreamRequestHandler(IMediator mediator, ITXT txt) : base(mediator, txt)
        {
        }

        public async Task<string> Handle(TXTFromStreamRequest request, CancellationToken cancellationToken)
        {
            var data = await GetOutputData(request.Reader);

            var result = OutputResult(new FinalOutputDataDto
            {
                Result = data.Result,
                ErrorMessage = data.ErrorMessage
            });

            return result;
        }

        public async Task<(FileInformationOutputDto Result, string ErrorMessage)> GetOutputData(StreamReader reader)
        {
            var fileData = await _TXT.ReadFile(reader);

            var result = await GetOutputs(new BaseOutputDataDto
            {
                Data = fileData.Data,
                ErrorMessage = fileData.ErrorMessage,
            });

            return result;
        }
    }
}
