
using System;

namespace AirportUWPClient.Models
{
    public class Departure : BaseModel
    {
        public int FlightId { set; get; }
        public string FlightNumber { set; get; }
        public DateTime DepartureTime { set; get; }
    }
}
