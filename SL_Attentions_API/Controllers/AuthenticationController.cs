using Application.Pattern.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SL_Attentions_API.Controllers
{
    public class AuthenticationController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login-user")]
        public async Task<ActionResult<AuthDto>> Login(AuthCommand parameters)
        {
            return await Mediator.Send(parameters);
        }
    }
}
