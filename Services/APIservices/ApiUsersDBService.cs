using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services.APIservices
{
    public class ApiUsersDBService : ApiDBService
    {
        private HttpClient HttpRequest;

        public ApiUsersDBService(HttpClient httpClient)
        {
            HttpRequest = httpClient;
        }

        public async Task<bool> IsLoginDataValid(UserLoginData userLoginData)
        {
            var PostResult = await HttpRequest.PostAsJsonAsync("api/UsersDB/UserExist", userLoginData);
            var result = await PostResult.Content.ReadFromJsonAsync<bool>();
            
            return result;
        }
    }
}
