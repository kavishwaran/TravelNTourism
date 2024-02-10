using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelNTourism.Model;


namespace TravelNTourism.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

       // public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<LocalUsers> LocalUser { get; set; }
        //public DbSet<Villa> Villas { get; set; }
        //public DbSet<VillaNumber> VillaNumbers { get; set; }    
       
       
    }
}
