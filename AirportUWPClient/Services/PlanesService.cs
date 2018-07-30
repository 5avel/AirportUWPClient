using AirportUWPClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportUWPClient.Services
{
    public interface IPlanesService
    {
        Task<IEnumerable<Plane>> GetAll();
        Task<Plane> Update(Plane item);
        Task<Plane> Add(Plane item);
        Task<bool> Delete(int id);
    }
    public class PlanesService : BaseAirportService, IPlanesService
    {
        private string endPoint = "/planes";
        public async Task<IEnumerable<Plane>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<Plane>>(json);
        }

        public async Task<Plane> Update(Plane item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PutAsync(endPoint, item.Id, obj);
            return JsonConvert.DeserializeObject<Plane>(json);
        }

        public async Task<Plane> Add(Plane item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PostAsync(endPoint, obj);
            return JsonConvert.DeserializeObject<Plane>(json);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync(endPoint, id);
        }
    }
}
