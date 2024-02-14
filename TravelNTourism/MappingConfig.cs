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

            CreateMap<Vehicle, VehicleDto>();
            CreateMap<Vehicle, VehicleDto>().ReverseMap(); 

            CreateMap<Vehicle, VehicleUpdateDto>();
            CreateMap<Vehicle, VehicleUpdateDto>().ReverseMap();

            CreateMap<Booking, BookingDto>();
            CreateMap<Booking, BookingDto>().ReverseMap();

            CreateMap<Booking, BookingUpdateDto>();
            CreateMap<Booking, BookingUpdateDto>().ReverseMap();

            CreateMap<Payment, PaymentDto>();
            CreateMap<Payment, PaymentDto>().ReverseMap();

            CreateMap<Payment, PaymentUpdateDto>();
            CreateMap<Payment, PaymentUpdateDto>().ReverseMap();


            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
