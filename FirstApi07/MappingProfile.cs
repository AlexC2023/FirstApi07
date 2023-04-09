using AutoMapper;
using FirstApi07.DTOs;
using FirstApi07.DTOs.CreateUpdateObjects;

namespace FirstApi07
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Announcement, CreateUpdateAnnouncement>().ReverseMap();
        }
    }
}
