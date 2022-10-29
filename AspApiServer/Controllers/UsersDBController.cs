using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Database_services;

namespace ASPcoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersDBController : ControllerBase
    {
        private DBservice DatabaseService;

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

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<bool>> RegisterUser(UserLoginData UserDataToRegister)
        {
            bool result = DatabaseService.InsertUser(UserDataToRegister);
            return Ok(result);
        }
    }
}
