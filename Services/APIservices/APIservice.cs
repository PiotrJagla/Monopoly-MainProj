using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services.API_services
{
    public class APIservice : IAPIservice
    {

        public HttpClient HTTPclient { get; private set; }

        public APIservice(HttpClient http)
        {
            
            HTTPclient = http;
        }

        public async Task<string> GetMessage()
        {
            var result = await HTTPclient.GetStringAsync("api/test");

            if(result is null)
            {
                throw new Exception("Message not found");
            }

            return result;
        }
    }
}
