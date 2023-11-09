using AutoMapper;
using System.Infrastructure.DTO;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RolPermissions, RolPermissionsDTO>().ReverseMap();
            CreateMap<Country, CountryDtos>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Comuna, ComunaDto>().ReverseMap();
            CreateMap<People, PeopleDto>().ReverseMap();
            CreateMap<SocialHelp, SocialHelpDto>().ReverseMap();
            CreateMap<ServicesByPeople, ServicesByPeopleDto>().ReverseMap();
        }

    }

}
