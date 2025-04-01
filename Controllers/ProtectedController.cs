using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Controllers
{
    [Route("api/protected")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        [HttpGet("Admin")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetAdminData()
        {
            return Ok(new { message = "Admin only" });
        }

        [HttpGet("User")]
        [Authorize(Policy = "UserPolicy")]
        public IActionResult GetUserData()
        {
            return Ok(new { message = "User only" });
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetData()
        {
            return Ok(new { message = "Accessible by both" });
        }
    }
}





        