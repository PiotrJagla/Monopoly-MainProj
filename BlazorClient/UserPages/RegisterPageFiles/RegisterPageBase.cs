using Microsoft.AspNetCore.Components;
using Models;
using Services.APIservices;

namespace BlazorClient.UserPages.RegisterPageFiles
{
    public class RegisterPageBase : ComponentBase
    {
        protected string RegisterMessage;

        [Inject]
        public ApiDBService InsertUserDataIntoDB { get; set; }

        protected override void OnInitialized()
        {
            RegisterMessage = "";
        }

        protected async Task RegisterUser(UserLoginData userLoginData)
        {
            if (await InsertUserDataIntoDB.RegisterUser(userLoginData) == false)
                RegisterMessage = "Failed to Register";
            else
                RegisterMessage = "Registration sukcesful";

            StateHasChanged();
        }
    }
}
