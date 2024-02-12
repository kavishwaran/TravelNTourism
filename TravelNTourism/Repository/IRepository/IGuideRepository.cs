using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository.IRepository
{
    public interface IGuideRepository : IRepository<Guide>
    {
      
         void UpdateAsync(GuideUpdateDto entity);
         void DeleteAsync(int id);
       






    }
}
