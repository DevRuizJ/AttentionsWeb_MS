using Microsoft.AspNetCore.Mvc;
using Security.Authentication;

namespace SL_Attentions_API.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet("@me")]
        public IActionResult MeData()
        {
            return Ok(User.GetUser());
        }
    }
}
