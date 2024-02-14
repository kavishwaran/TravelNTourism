namespace TravelNTourism.Data
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string BookingType { get; set; }  
        public int BookingTypeId { get; set; }    
        public int CustomerId { get; set; }    
        public int RoomId { get; set; }   
        public int VehicleId { get; set; }   
        public int GuideId { get; set; }
        public string UserId { get; set; }

    }
}
