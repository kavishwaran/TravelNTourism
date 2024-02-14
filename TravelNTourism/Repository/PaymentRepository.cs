using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       
        public async void UpdateAsync(PaymentUpdateDto entity)
        {
            var objFromDb = _db.Payments.FirstOrDefault(a => a.Id == entity.Id);
            if (objFromDb != null) 
            {
                objFromDb.UserId = entity.UserId; 
                objFromDb.Amount = entity.Amount;
                objFromDb.PaymentDate = entity.PaymentDate;
                objFromDb.MOP = entity.MOP;
                objFromDb.TransactionId = entity.TransactionId;
                objFromDb.Status = entity.Status;
                objFromDb.CVCNo = entity.CVCNo;
                objFromDb.CardExpiryNo = entity.CardExpiryNo;
                objFromDb.CardNo = entity.CardNo;
                objFromDb.BankName = entity.BankName;
                objFromDb.NameOnCard = entity.NameOnCard;

            }

        }
        public async void DeleteAsync(int id)
        {
            var objFromDb = _db.Guide.FirstOrDefault(a => a.Id == id);
            if (objFromDb != null)
            { 
                objFromDb.IsActive = "N"; 
            }
            //_db.Restaurants.Update(objFromDb);
            //await _db.SaveChangesAsync();
        }

    }
}
