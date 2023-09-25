using ImagineApps.Infrastructure.ExternalResources.Contracts;
using ImagineApps.Infrastructure.ExternalResources;
using MediatR;

namespace ImagineApps.Application.Features.FileHandler.Commands.TXTFromStream
{
    public class TXTFromStreamRequest : IRequest<string>
    {
        public StreamReader Reader { get; set; }
    }
}
