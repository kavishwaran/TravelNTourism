using Microsoft.AspNetCore.Identity;

namespace TravelNTourism.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
