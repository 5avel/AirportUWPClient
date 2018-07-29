namespace AirportUWPClient.Models
{
    using System;
    public class Flight : BaseModel
    {
        public string FlightNumber { set; get; }
        public string DeparturePoint { set; get; }
        public DateTime DepartureTime { set; get; }
        public string DestinationPoint { set; get; }
        public DateTime ArrivalTime { set; get; }
    }
}
