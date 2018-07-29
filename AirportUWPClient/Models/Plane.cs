namespace AirportUWPClient.Models
{
    using System;
    public class Plane : BaseModel
    {
        public int DepartureId { get; set; }
        public string Name { set; get; }
        public int PlaneTypeId { set; get; }
        public DateTime ReleaseDate { set; get; }
    }
}
