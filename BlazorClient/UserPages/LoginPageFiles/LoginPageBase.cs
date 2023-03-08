using BlazorClient.Components.UserLoginDataInputFiles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.UsersManagment;
using MySqlX.XDevAPI.Relational;
using Services.APIservices;
using System.Diagnostics;

namespace BlazorClient.UserPages.LoginPageFiles
{
    public class LoginPageBase : ComponentBase
    {
        [Inject]
        public ApiDBService CheckInput { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public List<string> Messages { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Messages = new List<string>();
        }

        protected async Task LoginUser(UserLoginData userLoginData)
        {
            try
            {
                if (await CheckInput.IsLoginDataValid(userLoginData) == false)
                    Messages.Add("Failed to login");
                else
                {
                    if (await CheckInput.IsUserAlreadyLogged(userLoginData) == true)
                        NavManager.NavigateTo($"/MainMenu/{userLoginData.Name}");
                    else
                        Messages.Add("Someone is logged on this account");
                }

            }
            catch
            {
                Messages.Add(Consts.Message.ServerDown);
            }

            StateHasChanged();
        }
    }
}
