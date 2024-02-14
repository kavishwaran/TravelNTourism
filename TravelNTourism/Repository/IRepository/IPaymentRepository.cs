using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
      
         void UpdateAsync(PaymentUpdateDto entity);
         void DeleteAsync(int id);
       






    }
}
