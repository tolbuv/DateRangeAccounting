using AutoMapper;
using DateRangeAccounting.API.Models;
using DateRangeAccounting.DAL.Domain;

namespace DateRangeAccounting.API.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DateRange, DateRangeViewModel>().ReverseMap();
        }
    }
}