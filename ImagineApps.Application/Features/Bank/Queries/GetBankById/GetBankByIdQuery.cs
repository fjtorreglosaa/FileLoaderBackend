using ImagineApps.Application.Utilities.Dtos;
using MediatR;

namespace ImagineApps.Application.Features.Bank.Queries.GetBankById
{
    public class GetBankByIdQuery : IRequest<BankDto>
    {
        public string BankId { get; set; }
    }
}
