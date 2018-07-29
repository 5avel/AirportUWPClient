namespace AirportUWPClient.Models
{
    using System;
    public class PlaneType : BaseModel
    {
        public string Model { set; get; }
        public int Seats { set; get; }
        public int Capacity { set; get; }
        public int Range { set; get; }
        public TimeSpan ServiceLife { set; get; }
    }
}