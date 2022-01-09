using AutoMapper;
using DataParserService.Dtos;
using DataParserService.Models;
using System.Linq;

namespace DataParserService.Profiles
{
    public class DataParserProfile : Profile
    {
        public DataParserProfile()
        {
            //Source -> Target
            CreateMap<Multiplicator, MultiplicatorReadDto>()
                .ForMember(dest => dest.IndexKey, opt => opt.MapFrom(src => src.Indexes.Select(s => s.Key)))
                .ForMember(dest => dest.IndexValue, opt => opt.MapFrom(src => src.Indexes.Select(s => s.Value)));
            CreateMap<MultiplicatorCreateDto, Multiplicator>();
            CreateMap<Multiplicator, MultiplicatorPublishedDto>()
                .ForMember(dest => dest.IndexKey, opt => opt.MapFrom(src => src.Indexes.Select(s => s.Key)))
                .ForMember(dest => dest.IndexValue, opt => opt.MapFrom(src => src.Indexes.Select(s => s.Value)));

            CreateMap<Company, CompanyReadDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyReadDto, CompanyPublishedDto>();
            CreateMap<Company, CompanyPublishedDto>()
                .ForMember(dest => dest.SecId, opt => opt.MapFrom(src => src.SecuritieTQBR.SECID));
        }
    }
}
