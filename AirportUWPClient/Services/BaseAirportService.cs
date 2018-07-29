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
        protected string baseURL = "http://localhost:5000/api";

        protected async Task<string> GetAsync(string url)
        {
            //string result;
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(new Uri(baseURL + url));
               
            }
            //return result;
        }

        protected async Task<string> PostAsync(string url, string body)
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.PostAsync(new Uri(url), new StringContent(body, Encoding.UTF8, "application/json")).ConfigureAwait(false))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }


       


    }
}
