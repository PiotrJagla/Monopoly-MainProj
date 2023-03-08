using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.UsersManagment;
using Services.Database_services;

namespace ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersDBController : ControllerBase
    {
        private DBservice DatabaseService;
        private static List<string> AllLoggedUsers = new List<string>();

        public UsersDBController(DBservice dbservice)
        {
            DatabaseService = dbservice;
        }

        [HttpPost("UserExist")]
        public async Task<ActionResult<bool>> UserExist(UserLoginData userLoginData)
        {
            bool result = DatabaseService.UserExist(userLoginData);
            return Ok(result);
        }

        [HttpPost("IsUserAlreadyLogged")]
        public async Task<ActionResult<bool>> IsUserAlreadyLogged(UserLoginData data)
        {
            bool Result = false;
            if(AllLoggedUsers.Contains(data.Name) == false)
            {
                AllLoggedUsers.Add(data.Name);
                Result = true;
            }

            return Ok(Result);
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<bool>> RegisterUser(UserLoginData UserDataToRegister)
        {
            bool result = DatabaseService.InsertUser(UserDataToRegister);
            return Ok(result);
        }

    }
}
