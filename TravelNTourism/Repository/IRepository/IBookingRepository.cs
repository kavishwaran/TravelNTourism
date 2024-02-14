using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
      
         void UpdateAsync(BookingUpdateDto entity);
         void DeleteAsync(int id);
       






    }
}
