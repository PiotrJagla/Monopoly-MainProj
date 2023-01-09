using Microsoft.AspNetCore.Components;
using Models;
using Models.UsersManagment;
using Services.APIservices;

namespace BlazorClient.UserPages.RegisterPageFiles
{
    public class RegisterPageBase : ComponentBase
    {
        [Inject]
        public ApiDBService InsertUserDataIntoDB { get; set; }

        public List<string> Messages { get; set; }

        protected override void OnInitialized()
        {
            Messages = new List<string>();
        }

        protected async Task RegisterUser(UserLoginData userLoginData)
        {
            try
            {
                if (await InsertUserDataIntoDB.RegisterUser(userLoginData) == false)
                    Messages.Add("Failed to Register");
                else
                    Messages.Add("Secessfully registered");
            }
            catch
            {
                Messages.Add(Consts.Message.ServerDown);
            }
            StateHasChanged();
        }
    }
}
