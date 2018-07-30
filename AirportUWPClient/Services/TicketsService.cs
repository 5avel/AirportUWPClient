using AirportUWPClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportUWPClient.Services
{
    public interface ITicketsService
    {
        Task<IEnumerable<Ticket>> GetAll();
        Task<Ticket> Buy(int id);
        Task<Ticket> Return(int id);
    }
    public class TicketsService : BaseAirportService, ITicketsService
    {
        private string endPoint = "/tickets";
        public async Task<IEnumerable<Ticket>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<Ticket>>(json);
        }

        public async Task<Ticket> Buy(int id)
        {
            string json = await GetAsync($"{endPoint}/Buy/{id}");
            return JsonConvert.DeserializeObject<Ticket>(json);
        }

        public async Task<Ticket> Return(int id)
        {
            string json = await GetAsync($"{endPoint}/Return/{id}");
            return JsonConvert.DeserializeObject<Ticket>(json);
        }


    }
}
