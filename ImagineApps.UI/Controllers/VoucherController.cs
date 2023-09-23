using ImagineApps.Application.Features.FileHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImagineApps.UI.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IMediator _mediator;

        public VoucherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult GenerateVoucher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateVoucher(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            var result = await _mediator.Send(new TXTRequest { filePath = filePath });

            return View(result);
        }
    }
}
