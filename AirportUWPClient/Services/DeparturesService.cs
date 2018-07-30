using AirportUWPClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AirportUWPClient.Services
{
    public interface IDeparturesService
    {
        Task<IEnumerable<Departure>> GetAll();
        Task<Departure> Update(Departure item);
        Task<Departure> Add(Departure item);
        Task<bool> Delete(int id);
    }
    public class DeparturesService : BaseAirportService, IDeparturesService
    {
        private string endPoint = "/departures";
        public async Task<IEnumerable<Departure>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<Departure>>(json);
        }

        public async Task<Departure> Update(Departure item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PutAsync(endPoint, item.Id, obj);
            return JsonConvert.DeserializeObject<Departure>(json);
        }

        public async Task<Departure> Add(Departure item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PostAsync(endPoint, obj);
            return JsonConvert.DeserializeObject<Departure>(json);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync(endPoint, id);
        }
    }
}
