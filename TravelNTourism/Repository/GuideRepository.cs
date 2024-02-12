using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository
{
    public class GuideRepository : Repository<Guide>, IGuideRepository
    {
        private readonly ApplicationDbContext _db;

        public GuideRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       
        public async void UpdateAsync(GuideUpdateDto entity)
        {
            var objFromDb = _db.Guide.FirstOrDefault(a => a.Id == entity.Id);
            if (objFromDb != null) 
            { 
                objFromDb.UserId = entity.UserId;
                objFromDb.Name = entity.Name;
                objFromDb.TpNo = entity.TpNo;
                objFromDb.Image = entity.Image;
                objFromDb.Descriptiohn = entity.Descriptiohn; 
                objFromDb.Language = entity.Language; 
                objFromDb.Email = entity.Email;
                //_db.Restaurants.Update(objFromDb);
                //await _db.SaveChangesAsync();
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
