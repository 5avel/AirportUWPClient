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
    public interface IStewardessesService
    {
        Task<IEnumerable<Stewardess>> GetAll();
        Task<Stewardess> Update(Stewardess item);
        Task<Stewardess> Add(Stewardess item);
        Task<bool> Delete(int id);
    }

    class StewardessesService : BaseAirportService, IStewardessesService
    { 

            private string endPoint = "/stewardesses";
            public async Task<IEnumerable<Stewardess>> GetAll()
            {
                string json = await GetAsync(endPoint);
                return JsonConvert.DeserializeObject<IEnumerable<Stewardess>>(json);
            }

            public async Task<Stewardess> Update(Stewardess item)
            {
                string obj = JsonConvert.SerializeObject(item);
                string json = await PutAsync(endPoint, item.Id, obj);
                return JsonConvert.DeserializeObject<Stewardess>(json);
            }

            public async Task<Stewardess> Add(Stewardess item)
            {
                string obj = JsonConvert.SerializeObject(item);
                string json = await PostAsync(endPoint, obj);
                return JsonConvert.DeserializeObject<Stewardess>(json);
            }

            public async Task<bool> Delete(int id)
            {
                return await DeleteAsync(endPoint, id);
            }
        
    }
}
