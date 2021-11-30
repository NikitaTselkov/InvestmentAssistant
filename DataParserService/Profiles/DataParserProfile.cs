using AutoMapper;
using DataParserService.Dtos;
using DataParserService.Models;

namespace DataParserService.Profiles
{
    public class DataParserProfile : Profile
    {
        public DataParserProfile()
        {
            //Source -> Target
            CreateMap<Multiplicator, MultiplicatorReadDto>();
            CreateMap<MultiplicatorCreateDto, Multiplicator>();
            CreateMap<Multiplicator, MultiplicatorPublishedDto>();

            CreateMap<Company, CompanyReadDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyReadDto, CompanyPublishedDto>();
        }
    }
}
