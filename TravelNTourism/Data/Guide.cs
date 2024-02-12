namespace TravelNTourism.Data
{
    public class Guide
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? TpNo { get; set; }
        public string? Image { get; set; }
        public string? Descriptiohn { get; set; }
        public string? IsActive { get; set; } 
        public string? Language { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
