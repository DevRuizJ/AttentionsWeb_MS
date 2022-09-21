using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Auth
{
    public class AuthRequest : IMapFrom<AuthCommand>
    {
        public string User { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthCommand, AuthRequest>();
        }
    }
}
