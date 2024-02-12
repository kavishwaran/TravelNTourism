using AutoMapper;
using TravelNTourism.Data;
using TravelNTourism.Model;
using TravelNTourism.Model.Dto;

namespace TravelNTourism
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Restaurant, RestaurantDto>(); 
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();

            CreateMap<Restaurant, RestaurantUpdateDto>();
            CreateMap<Restaurant, RestaurantUpdateDto>().ReverseMap();


            CreateMap<Guide, GuideDto>();
            CreateMap<Guide, GuideDto>().ReverseMap();

            CreateMap<Restaurant, GuideUpdateDto>();
            CreateMap<Restaurant, GuideUpdateDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
