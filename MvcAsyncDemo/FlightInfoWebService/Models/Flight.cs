namespace FlightInfoWebService.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public decimal Price { get; set; }
    }
}