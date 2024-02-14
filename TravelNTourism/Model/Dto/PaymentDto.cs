﻿namespace TravelNTourism.Model.Dto
{
    public class PaymentDto
    {
        // public int Id { get; set; }             
        public string UserId { get; set; }
        public int BookingId { get; set; }             
        public decimal Amount { get; set; }         
        public DateTime PaymentDate { get; set; }    
        public string MOP { get; set; }   
        public string TransactionId { get; set; }     
        public string Status { get; set; }
        public string CVCNo { get; set; }
        public string CardExpiryNo { get; set; }
        public string CardNo { get; set; }
        public string BankName { get; set; }
        public string NameOnCard { get; set; }
    }
}
