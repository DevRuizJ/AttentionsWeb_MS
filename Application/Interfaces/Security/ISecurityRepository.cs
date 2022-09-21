using Application.Commom.Status;
using Application.Pattern.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Security
{
    public interface ISecurityRepository
    {
        Task<(ServiceStatus, string, AuthResponse)> Authentication(AuthRequest request);
    }
}
