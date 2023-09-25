using ImagineApps.Application.Features.FileHandler.Commands.TXTFromPath;
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
            if (customPath != null)
            {
                var result = await _mediator.Send(new TXTFromPathRequest { filePath = customPath });

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

                                var fileContent = await reader.ReadToEndAsync();

                                return Ok($"File content: {fileContent}");
                            }
                        }
                    }

                    return BadRequest("File is empty");
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return BadRequest();
        }
    }
}
