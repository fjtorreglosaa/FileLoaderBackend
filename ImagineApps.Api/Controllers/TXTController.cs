using ImagineApps.Api.Models;
using ImagineApps.Application.Features.FileHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImagineApps.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TXTController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TXTController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateVoucher([FromBody] PathModel model)
        {
            var result = await _mediator.Send(new TXTRequest { filePath = model.Path });

            return Ok(result);

        }
    }
}
