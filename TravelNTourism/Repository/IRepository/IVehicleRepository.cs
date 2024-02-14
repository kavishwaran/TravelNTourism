using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository.IRepository
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
      
         void UpdateAsync(VehicleUpdateDto entity);
         void DeleteAsync(int id);
       






    }
}
