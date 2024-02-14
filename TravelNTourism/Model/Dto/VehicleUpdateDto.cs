namespace TravelNTourism.Data
{
    public class VehicleUpdateDto
    {
         public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public double IsActive { get; set; }
    }
}
