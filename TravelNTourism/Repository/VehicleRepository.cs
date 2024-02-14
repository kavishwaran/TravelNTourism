using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _db;

        public VehicleRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       
        public async void UpdateAsync(VehicleUpdateDto entity)
        {
            var objFromDb = _db.Vehicle.FirstOrDefault(a => a.Id == entity.Id);
            if (objFromDb != null) 
            {
               // objFromDb.Id = entity.Id;
                objFromDb.Make = entity.Make;
                objFromDb.Model = entity.Model;
                objFromDb.Year = entity.Year;
                objFromDb.Color = entity.Color;
                objFromDb.Price = entity.Price;

            }

        }
        public async void DeleteAsync(int id)
        {
            var objFromDb = _db.Vehicle.FirstOrDefault(a => a.Id == id);
            if (objFromDb != null)
            { 
                objFromDb.IsActive = "N"; 
            }
            //_db.Restaurants.Update(objFromDb);
            //await _db.SaveChangesAsync();
        }

    }
}
