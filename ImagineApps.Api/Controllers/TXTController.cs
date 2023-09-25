using ImagineApps.Application.Features.FileHandler.Commands.TXTFromPath;
using ImagineApps.Application.Features.FileHandler.Commands.TXTFromStream;
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
        public async Task<IActionResult> UploadFile(string customPath = null)
        {
            string result;

            if (customPath != null)
            {
                result = await _mediator.Send(new TXTFromPathRequest { filePath = customPath });

                return Ok(result);
            }
            else
            {
                try
                {
                    var file = Request.Form.Files[0]; 

                    if (file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin);

                            using (var reader = new StreamReader(memoryStream))
                            {
                                result = await _mediator.Send(new TXTFromStreamRequest { Reader = reader });

                                return Ok(result);
                            }
                        }
                    }

                    return BadRequest("File is empty");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }
}
