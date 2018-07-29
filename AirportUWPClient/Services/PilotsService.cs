using AirportUWPClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AirportUWPClient.Services
{
    public interface IPilotsService
    {
        Task<IEnumerable<Pilot>> GetAll();
        Task<Pilot> Update(Pilot item);
    }


    public class PilotsService : BaseAirportService, IPilotsService
    {
        public PilotsService()
        {

        }

        private string endPoint = "/pilots";


        public async Task<IEnumerable<Pilot>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<Pilot>>(json);
        }


        public async Task<Pilot> Update(Pilot item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PutAsync(endPoint, item.Id, obj);
            return JsonConvert.DeserializeObject<Pilot>(json);
        }

       
    }
}
