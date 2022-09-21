using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.Attention;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Read.List
{
    public class AttentionListCommand : IRequest<AttentionListDto>
    {
        public string Compania { get; set; }
        public string Sucursal { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<AttentionListCommand>
    {
        public ExecuteValidation()
        {
            // VALIDATION
            RuleFor(c => c.Compania).NotEmpty();
        }
    }

    public class Manejador : IRequestHandler<AttentionListCommand, AttentionListDto>
    {
        private readonly IAttentionRepository _attentionService;
        private readonly IMapper _mapper;

        public Manejador(IAttentionRepository atencionService, IMapper mapper)
        {
            _attentionService = atencionService;
            _mapper = mapper;
        }
        public async Task<AttentionListDto> Handle(AttentionListCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _attentionService.AttentionList(_mapper.Map<AttentionListRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<AttentionListDto>(response);

           return result;
        }
    }
}
