using AutoMapper;
using ImagineApps.Application.Utilities.Dtos;
using ImagineApps.Infrastructure.UnitOfWork.Contracts;
using MediatR;

namespace ImagineApps.Application.Features.Bank.Queries.GetBankById
{
    public class GetBankByIdQueryHandler : IRequestHandler<GetBankByIdQuery, BankDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBankByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BankDto> Handle(GetBankByIdQuery request, CancellationToken cancellationToken)
        {
            using (var context = _unitOfWork.Create())
            {
                var bank = await context.Repositories.BankRepository.GetByIdAsync(request.BankId);

                var result = _mapper.Map<BankDto>(bank);

                return result;
            }
        }
    }
}
