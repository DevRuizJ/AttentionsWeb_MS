using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.Service;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Service.Read.Filter
{
    public class ServiceFilterCommand : IRequest<IEnumerable<ServiceFilterDto>>
    {
        public string Compania { get; set; }
        public string Filtro { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<ServiceFilterCommand>
    {
        public ExecuteValidation()
        {
            // Validate Fluent Validation
            RuleFor(c => c.Compania).NotEmpty();//.Length(7);
            RuleFor(c => c.Filtro).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<ServiceFilterCommand, IEnumerable<ServiceFilterDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public Handler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceFilterDto>> Handle(ServiceFilterCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _serviceRepository.ServiceFilter(_mapper.Map<ServiceFilterRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<ICollection<ServiceFilterDto>>(response);

            return result;
        }
    }
}
