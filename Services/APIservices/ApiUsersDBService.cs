using Models.UsersManagment;
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
            var DataValidationHttpResponse = await HttpRequest.PostAsJsonAsync("api/UsersDB/UserExist", userLoginData);
            var IsUserLogged = await DataValidationHttpResponse.Content.ReadFromJsonAsync<bool>();
            return IsUserLogged;
        }

        public async Task<bool> IsUserLogged(UserLoginData userLoginData)
        {
            var IsUserLoggedResponse = await HttpRequest.PostAsJsonAsync("api/UsersDB/IsUserLogged", userLoginData);
            var IsUserLogged = await IsUserLoggedResponse.Content.ReadFromJsonAsync<bool>();
            return IsUserLogged;
        }

        public async Task<bool> RegisterUser(UserLoginData UserDataToRegister)
        {
            var UserRegistrationResponse = await HttpRequest.PostAsJsonAsync("api/UsersDB/RegisterUser", UserDataToRegister);
            var IsUserRegistered = await UserRegistrationResponse.Content.ReadFromJsonAsync<bool>();
            return IsUserRegistered;
        }
    }
}
