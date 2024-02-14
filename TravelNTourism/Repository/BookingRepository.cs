using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       
        public async void UpdateAsync(BookingUpdateDto entity)
        {
            var objFromDb = _db.Bookings.FirstOrDefault(a => a.Id == entity.Id);
            if (objFromDb != null) 
            {
                objFromDb.BookingDate = entity.BookingDate;
                objFromDb.CustomerName = entity.CustomerName;
                objFromDb.ContactNumber = entity.ContactNumber;
                objFromDb.CheckInDate = entity.CheckInDate;
                objFromDb.CheckOutDate = entity.CheckOutDate;
                objFromDb.NumberOfGuests = entity.NumberOfGuests;
                objFromDb.BookingType = entity.BookingType;
                objFromDb.BookingTypeId = entity.BookingTypeId;
                objFromDb.CustomerId = entity.CustomerId;
                objFromDb.RoomId = entity.RoomId;
                objFromDb.VehicleId = entity.VehicleId;
                objFromDb.GuideId = entity.GuideId;
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
