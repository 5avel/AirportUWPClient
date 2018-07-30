using AirportUWPClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportUWPClient.Services
{
    public interface IPlaneTypesService
    {
        Task<IEnumerable<PlaneType>> GetAll();
        Task<PlaneType> Update(PlaneType item);
        Task<PlaneType> Add(PlaneType item);
        Task<bool> Delete(int id);
    }
    public class PlaneTypesService : BaseAirportService, IPlaneTypesService
    {
        private string endPoint = "/planeTypes";
        public async Task<IEnumerable<PlaneType>> GetAll()
        {
            string json = await GetAsync(endPoint);
            return JsonConvert.DeserializeObject<IEnumerable<PlaneType>>(json);
        }

        public async Task<PlaneType> Update(PlaneType item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PutAsync(endPoint, item.Id, obj);
            return JsonConvert.DeserializeObject<PlaneType>(json);
        }

        public async Task<PlaneType> Add(PlaneType item)
        {
            string obj = JsonConvert.SerializeObject(item);
            string json = await PostAsync(endPoint, obj);
            return JsonConvert.DeserializeObject<PlaneType>(json);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync(endPoint, id);
        }
    }
}
