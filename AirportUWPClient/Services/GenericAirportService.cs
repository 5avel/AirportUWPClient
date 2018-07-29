using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AirportUWPClient.Services
{
    public class GenericAirportService
    {
        private string baseURL = "http://localhost/api";

        private async Task<string> GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.GetAsync(new Uri(baseURL + url)))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }

        private async Task<string> PostAsync(string url, string body)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.PostAsync(new Uri(url), new StringContent(body, Encoding.UTF8, "application/json")))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }

        public async  Task<IEnumerable<T>> GetAll<T>()
        {
            return JsonConvert.DeserializeObject< IEnumerable<T>>(await GetAsync("/pilots"));
        }
    }
}
