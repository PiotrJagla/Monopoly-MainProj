using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<string>> getTestMessage()
        {
            return Ok("Test message from API");
        }

    }
}
