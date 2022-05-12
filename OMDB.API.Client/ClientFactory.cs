using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMDB.API.Client
{
    public static class ClientFactory
    {
        public static T CreateClient<T>(HttpClient httpClient, string apikey)
            where T : ClientBase
        {

            return Activator.CreateInstance(typeof(T), httpClient, apikey) as T;
        }
    }
}
