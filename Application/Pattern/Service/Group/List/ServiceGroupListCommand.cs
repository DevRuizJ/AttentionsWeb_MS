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

namespace Application.Pattern.Service.Group.List
{
    public class ServiceGroupListCommand : IRequest<IEnumerable<ServiceGroupListDto>>
    {
        public string Compania { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<ServiceGroupListCommand>
    {
        public ExecuteValidation()
        {
            // Validate Fluent Validation
            RuleFor(c => c.Compania).NotEmpty();//.Length(7);
        }
    }

    public class Handler : IRequestHandler<ServiceGroupListCommand, IEnumerable<ServiceGroupListDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public Handler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceGroupListDto>> Handle(ServiceGroupListCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _serviceRepository.GetServiceGroupList(_mapper.Map<ServiceGroupListRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<ICollection<ServiceGroupListDto>>(response);

            return result;
        }
    }
}
