using AirportUWPClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportUWPClient.Services
{
    public interface ITicketsService
    {
        Task<IEnumerable<Ticket>> GetAll();
        Task<Ticket> Update(Ticket item);
        Task<Ticket> Add(Ticket item);
        Task<bool> Delete(int id);
    }
    public class TicketsService : BaseAirportService, ITicketsService
    {
        private string endPoint = "/tickets";
        public async Task<IEnumerable<Ticket>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<Ticket>>(json);
        }

        public async Task<Ticket> Update(Ticket item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PutAsync(endPoint, item.Id, obj);
            return JsonConvert.DeserializeObject<Ticket>(json);
        }

        public async Task<Ticket> Add(Ticket item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PostAsync(endPoint, obj);
            return JsonConvert.DeserializeObject<Ticket>(json);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync(endPoint, id);
        }
    }
}
