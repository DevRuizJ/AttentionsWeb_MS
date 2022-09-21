using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.Security;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Auth
{
    public class AuthCommand : IRequest<AuthDto>
    {
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<AuthCommand>
    {
        public ExecuteValidation()
        {
            RuleFor(x => x.User).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<AuthCommand, AuthDto>
    {
        private readonly IMapper _mapper;
        private readonly ISecurityRepository _securityRepository;
        private readonly IJwtGenerator _jwtGenerator;

        public Handler(IMapper mapper, ISecurityRepository securityRepository, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _securityRepository = securityRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthDto> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _securityRepository.Authentication(_mapper.Map<AuthRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = await _jwtGenerator.CreateToken(response);

            return result;
        }
    }
}
