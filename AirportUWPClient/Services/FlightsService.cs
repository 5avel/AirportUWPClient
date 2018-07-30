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
    public interface IFlightsService
    {
        Task<IEnumerable<Flight>> GetAll();
        Task<Flight> Update(Flight item);
        Task<Flight> Add(Flight item);
        Task<bool> Delete(int id);
    }
    class FlightsService : BaseAirportService, IFlightsService
    {
        private string endPoint = "/flights";
        public async Task<IEnumerable<Flight>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<Flight>>(json);
        }

        public async Task<Flight> Update(Flight item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PutAsync(endPoint, item.Id, obj);
            return JsonConvert.DeserializeObject<Flight>(json);
        }

        public async Task<Flight> Add(Flight item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PostAsync(endPoint, obj);
            return JsonConvert.DeserializeObject<Flight>(json);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync(endPoint, id);
        }
    }
}
