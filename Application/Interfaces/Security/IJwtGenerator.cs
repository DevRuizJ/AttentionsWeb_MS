using Application.Pattern.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Security
{
    public interface IJwtGenerator
    {
        Task<AuthDto> CreateToken(AuthResponse auth);
    }
}
