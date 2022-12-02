using Microsoft.AspNetCore.Components;
using Models.UsersManagment;

namespace BlazorClient.Components.UserLoginDataInputFiles
{
    public class UserLoginDataInputBase : ComponentBase
    {
        
        protected UserLoginData userLoginDataInput;


        [Parameter]
        public EventCallback<UserLoginData> GetUserLoginDataInput { get; set; }

        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public string ButtonName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userLoginDataInput = new UserLoginData();
        }

        protected void SendUserLoginDataInput()
        {
            GetUserLoginDataInput.InvokeAsync(userLoginDataInput);
        }
    }
}
