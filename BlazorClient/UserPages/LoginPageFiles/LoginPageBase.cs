using BlazorClient.Components.UserLoginDataInputFiles;
using Microsoft.AspNetCore.Components;
using Models.UsersManagment;
using Services.APIservices;

namespace BlazorClient.UserPages.LoginPageFiles
{
    public class LoginPageBase : ComponentBase
    {
        [Inject]
        public ApiDBService validateUserLoginData { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public List<string> Messages { get; set; }

        protected override void OnInitialized()
        {
            Messages = new List<string>();
        }


        protected async void LoginUser(UserLoginData userLoginData)
        {
            try
            {
                if (await validateUserLoginData.IsLoginDataValid(userLoginData) == false)
                    Messages.Add("Failed to login");
                else
                    NavManager.NavigateTo($"/MainMenu/{userLoginData.Name}");
            }
            catch
            {
                Messages.Add("Database Service is not active");
            }
            StateHasChanged();
        }
    }
}
