using AutoMapper;
using ImagineApps.Application.Utilities.Dtos;
using ImagineApps.Domain.Entities;

namespace ImagineApps.Application.Utilities
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Bank, BankDto>().ReverseMap();
        }
    }
}
