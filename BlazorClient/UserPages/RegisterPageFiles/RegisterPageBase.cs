using Microsoft.AspNetCore.Components;
using Models.UsersManagment;
using Services.APIservices;

namespace ClientSide.UserPages.RegisterPageFiles
{
    public class RegisterPageBase : ComponentBase
    {
        [Inject]
        public ApiDBService InsertUserDataIntoDB { get; set; }

        protected string RegisterMessage;

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
