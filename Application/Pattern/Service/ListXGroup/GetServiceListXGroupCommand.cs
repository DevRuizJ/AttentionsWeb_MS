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

namespace Application.Pattern.Service.ListXGroup
{
    public class GetServiceListXGroupCommand : IRequest<IEnumerable<GetServiceListXGroupDto>>
    {
        public string Compania { get; set; }
        //public string IdGrupo { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<GetServiceListXGroupCommand>
    {
        public ExecuteValidation()
        {
            // Validate Fluent Validation
            RuleFor(c => c.Compania).NotEmpty();//.Length(4);
            //RuleFor(c => c.IdGrupo).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<GetServiceListXGroupCommand, IEnumerable<GetServiceListXGroupDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public Handler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetServiceListXGroupDto>> Handle(GetServiceListXGroupCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _serviceRepository.GetServiceListXGroup(_mapper.Map<GetServiceListXGroupRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<ICollection<GetServiceListXGroupDto>>(response);

            return result;
        }
    }
}
