using Microsoft.AspNetCore.Mvc;

namespace TestBogus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(FakeDateService fakeDateService) : ControllerBase
    {
        private readonly FakeDateService _fakeDateService = fakeDateService;

        [HttpGet]
        public IActionResult GetUser()
        {
            var user = _fakeDateService.GetUser();

            return Ok(user);
        }
    }
}
