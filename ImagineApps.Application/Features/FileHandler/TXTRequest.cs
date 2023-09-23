using MediatR;

namespace ImagineApps.Application.Features.FileHandler
{
    public class TXTRequest : IRequest<string>
    {
        public string filePath { get; set; }
    }
}
