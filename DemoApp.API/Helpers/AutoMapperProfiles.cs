using System.Linq;
using AutoMapper;
using DemoApp.API.Dtos;
using DemoApp.API.Models; 
namespace DemoApp.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForListdto>()
            .ForMember(dest => dest.PhotoUrl,
            opt=> opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest =>dest.Age,opt => 
            opt.MapFrom(src=> src.DateOfBirth.CalculateAge()));
            CreateMap<User,UserForDetailedDto>()
            .ForMember(dest => dest.PhotoUrl,
            opt=> opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest =>dest.Age,opt => 
            opt.MapFrom(src=> src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,photosForDetailedDto>();
        }
    }
}