using AirportUWPClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportUWPClient.Services
{
    public interface ICrewsService
    {
        Task<IEnumerable<Crew>> GetAll();
        Task<Crew> Update(Crew item);
        Task<Crew> Add(Crew item);
        Task<bool> Delete(int id);
    }
    public class CrewsService : BaseAirportService, ICrewsService
    {
        private string endPoint = "/pilots";
        public async Task<IEnumerable<Crew>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<Crew>>(json);
        }

        public async Task<Crew> Update(Crew item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PutAsync(endPoint, item.Id, obj);
            return JsonConvert.DeserializeObject<Crew>(json);
        }

        public async Task<Crew> Add(Crew item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PostAsync(endPoint, obj);
            return JsonConvert.DeserializeObject<Crew>(json);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync(endPoint, id);
        }
    }
}
