using AutoMapper;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;

namespace DataIntensiveWepApi
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {

            CreateMap<Customer, CustomerDTO>()
                   .ReverseMap();

            CreateMap<Site, SiteDTO>()
                .ForMember(d => d.Users,
                    opt => opt.MapFrom(s => s.SiteUsers.Select(su => su.UserUu)))
                .ReverseMap();

            CreateMap<Site, UpdateSiteDTO>()
                .ReverseMap();

            CreateMap<Device, DeviceDTO>()
                .ReverseMap();

            CreateMap<Measurement, MeasurementDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}
