using Application.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Security.Token.JwtGenerator;

namespace Security.Authentication
{
    public static class ClaimsPrincipalExtension
    {
        public static AuthenticatedUser GetUser(this ClaimsPrincipal principal)
        {
            var User = principal.FindFirstValue(JWTClaimTypes.User);
            var Name = principal.FindFirstValue(JWTClaimTypes.Name);
            var LastName = principal.FindFirstValue(JWTClaimTypes.LastName);
            var MotherLastname = principal.FindFirstValue(JWTClaimTypes.MotherLastName);

            return new AuthenticatedUser(User, Name, LastName, MotherLastname);
        }
    }
}
