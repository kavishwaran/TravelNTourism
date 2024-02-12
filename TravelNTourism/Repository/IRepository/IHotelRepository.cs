using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository.IRepository
{
    public interface IHotelRepository :IRepository<Restaurant>
    {
      
         void UpdateAsync(RestaurantUpdateDto entity);
         void DeleteAsync(int id);
       






    }
}
