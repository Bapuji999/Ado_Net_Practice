using Microsoft.AspNetCore.Mvc;

namespace ConnectionPracticeInADO.Net.Controllers
{
    [ApiController]
    public class TestConnectionString : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TestConnectionString(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("CheckConnection")]
        public string CheckConnection()
        {
            DataLayer Dl = new DataLayer(_configuration);
            string msg = Dl.Getdata();
            return msg;
        }
    }
}
