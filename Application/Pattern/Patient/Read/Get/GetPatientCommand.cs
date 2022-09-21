using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.Patient;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.Get
{
    public class GetPatientCommand : IRequest<GetPatientDto>
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
    }
    public class ExecuteValidation : AbstractValidator<GetPatientCommand>
    {
        public ExecuteValidation()
        {
            // Validate Fluent Validation

            RuleFor(c => c.TipoDocumento).NotEmpty();
            RuleFor(c => c.NumeroDocumento).NotEmpty();
        }
    }
    public class Handler : IRequestHandler<GetPatientCommand, GetPatientDto>
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IPatientService patientService )
        {
            _mapper = mapper;
            _patientService = patientService;

        }
        public async Task<GetPatientDto> Handle(GetPatientCommand request, CancellationToken cancellationToken)
        {

            var (status, message, response) = await _patientService.GetPatient(_mapper.Map<GetPatientRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<GetPatientDto>(response);

            return result;
        }
    }
}
