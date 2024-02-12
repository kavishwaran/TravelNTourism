﻿using TravelNTourism.Data;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository
{
    public class HotelRepository : Repository<Restaurant>, IHotelRepository
    {
        private readonly ApplicationDbContext _db;

        public HotelRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       
        public async void UpdateAsync(RestaurantUpdateDto entity)
        {
            var objFromDb = _db.Restaurants.FirstOrDefault(a => a.Id == entity.Id);
            if (objFromDb != null) 
            {  
                objFromDb.Longitude = entity.Longitude;
                objFromDb.Latitude = entity.Latitude;
                objFromDb.Name = entity.Name;
                objFromDb.Address = entity.Address;
                objFromDb.CreatedOn = entity.CreatedOn; 
                objFromDb.Reason = entity.Reason;
                objFromDb.ImageURl = entity.ImageURl;
                objFromDb.Description = entity.Description;
                objFromDb.HeaderName = entity.HeaderName;
                objFromDb.SubHeaderName = entity.SubHeaderName;
                objFromDb.Price = entity.Price;
                objFromDb.HasMoreInfo = entity.HasMoreInfo;
                //_db.Restaurants.Update(objFromDb);
                //await _db.SaveChangesAsync();
            }
      
        }
        public async void DeleteAsync(int id)
        {
            var objFromDb = _db.Restaurants.FirstOrDefault(a => a.Id == id);
            if (objFromDb != null)
            { 
                objFromDb.IsActive = "N"; 
            }
            //_db.Restaurants.Update(objFromDb);
            //await _db.SaveChangesAsync();
        }

    }
}
