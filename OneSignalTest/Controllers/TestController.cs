using Microsoft.AspNetCore.Mvc;

namespace OneSignalTest.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Test([FromQuery] string a)
        {
            return "Test " + a;
        }
    }
}

