namespace AirportUWPClient.Models
{
    public class Ticket : BaseModel
    {
        public int FlightId { set; get; }
        public string FlightNumber { set; get; }
        public decimal Price { set; get; }
        public int PlaseNumber { set; get; }
        public bool IsSold { set; get; }
    }
}