namespace AirportUWPClient.Models
{
    using System.Collections.Generic;
    public class Crew : BaseModel
    {
        public int? DepartureId { get; set; }
        public string Name { get; set; }
    }
}