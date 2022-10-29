using BlazorClient.Components.UserLoginDataInputFiles;
using Microsoft.AspNetCore.Components;
using Models;
using Services.APIservices;

namespace BlazorClient.UserPages.LoginPageFiles
{
    public class LoginPageBase : ComponentBase
    {
        [Inject]
        public ApiDBService validateUserLoginData { get; set; }

        public string LoginMessage { get; set; }

        protected override void OnInitialized()
        {
            LoginMessage = "";
        }


        protected async void LoginUser(UserLoginData userLoginData)
        {
            if(await validateUserLoginData.IsLoginDataValid(userLoginData) == false)
                LoginMessage = "Failed to login";
            else
                LoginMessage = "Login sukcesful";

            StateHasChanged();
        }
    }
}
