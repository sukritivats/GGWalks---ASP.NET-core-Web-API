using AutoMapper;
using GGWalks.API.Models.Domain;
using GGWalks.API.Models.DTO;

namespace GGWalks.API.Mappings
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            // when property name are different in src and dest
            //CreateMap<Region, RegionDto>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}
