using AssessmentInvestmentAttractivenessService.Dtos;
using AssessmentInvestmentAttractivenessService.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.Profiles
{
    public class DataParserProfile : Profile
    {
        public DataParserProfile()
        {
            //Source -> Target
            CreateMap<DbListNodeDto, DescriptionForMultiplicators>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Key))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Value));

            CreateMap<DbListNodeDto, GroupOfMultiplicators>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.GroupCode, opt => opt.MapFrom(src => src.Key))
                 .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Value));
            
            CreateMap<DbListNodeDto, FieldOfActivityOfCompany>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.FieldOfActivityCode, opt => opt.MapFrom(src => src.Key))
                 .ForMember(dest => dest.FieldOfActivityName, opt => opt.MapFrom(src => src.Value));
        }
    }
}
