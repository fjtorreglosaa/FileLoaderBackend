using MediatR;

namespace ImagineApps.Application.Features.FileHandler.Commands.TXTFromPath
{
    public class TXTFromPathRequest : IRequest<string>
    {
        public string filePath { get; set; }
    }
}
