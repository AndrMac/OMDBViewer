using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OMDB.API.Client
{
    public static class HttpClientHelper
    {
        public static HttpClient GetOMDbHttpClient(string baseUrl, string apiKey)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri($"{baseUrl}?apikey={apiKey}&"),
            };

           
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("apikey", apiKey);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("apikey", apiKey);
            return client;
        }
    }
}
