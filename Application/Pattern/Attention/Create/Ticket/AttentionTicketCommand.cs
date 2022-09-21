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

namespace Application.Pattern.Attention.Create.Ticket
{
    public class AttentionTicketCommand : IRequest<AttentionTicketDto>
    {
        public string OSNumber { get; set; }
        public string Period { get; set; }
        public string Year { get; set; }
        public string BranchOffice { get; set; }
        public string Company { get; set; }
        //public string Type { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<AttentionTicketCommand>
    {
        public ExecuteValidation()
        {
            //RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.OSNumber).NotEmpty();
            RuleFor(c => c.Period).NotEmpty();
            RuleFor(c => c.Year).NotEmpty();
            RuleFor(c => c.BranchOffice).NotEmpty();
            RuleFor(c => c.Company).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<AttentionTicketCommand, AttentionTicketDto>
    {
        private readonly IAttentionRepository _attentionService;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IAttentionRepository attentionService)
        {
            _attentionService = attentionService;
            _mapper = mapper;
        }

        public async Task<AttentionTicketDto> Handle(AttentionTicketCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _attentionService.AttentionTicket(_mapper.Map<AttentionTicketRequest>(request));

            if (status != ServiceStatus.Ok)
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError,
                        message
                    );

            var result = _mapper.Map<AttentionTicketDto>(response);

            return result;
        }
    }
}
