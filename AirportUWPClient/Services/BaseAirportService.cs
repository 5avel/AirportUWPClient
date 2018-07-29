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
    public class BaseAirportService
    {
        protected string baseURL = "http://airportresrfulapi20180729070437.azurewebsites.net/api";

        protected async Task<string> GetAsync(string url)
        {
            //string result;
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(baseURL + url));
               
            }
            //return result;
        }

        protected async Task<string> PostAsync(string endPoint, string body)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.PostAsync(new Uri($"{baseURL}{endPoint}"), new StringContent(body, Encoding.UTF8, "application/json")).ConfigureAwait(false))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }

        protected async Task<string> PutAsync(string endPoint, int id, string body)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.PutAsync(new Uri($"{baseURL}{endPoint}/{id}"), new StringContent(body, Encoding.UTF8, "application/json")).ConfigureAwait(false))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }





    }
}
