using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.Patient;
using Application.Pattern.Patient.Get;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.New
{
    public class NewPatientCommand : IRequest<NewPatientDto>
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string DiaNacimiento { get; set; }
        public string MesNacimiento { get; set; }
        public string AnioNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string TelefonoMovil1 { get; set; }

        public string ApellidoCasada { get; set; }
        public string EstadoCivil { get; set; }
        public string Direccion { get; set; }
        public string TelefonoMovil2 { get; set; }
        public string Nacionalidad { get; set; }

    }

    public class ExecuteValidation : AbstractValidator<NewPatientCommand>
    {
        public ExecuteValidation()
        {
            // Validate Fluent Validation            
            RuleFor(c => c.TipoDocumento).NotEmpty().Length(1);
            RuleFor(c => c.NumeroDocumento).NotEmpty();
            RuleFor(c => c.Nombre).NotEmpty();
            RuleFor(c => c.ApellidoPaterno).NotEmpty();
            RuleFor(c => c.ApellidoMaterno).NotEmpty();
            RuleFor(c => c.Sexo).NotEmpty();
            RuleFor(c => c.DiaNacimiento).NotEmpty();
            RuleFor(c => c.MesNacimiento).NotEmpty();
            RuleFor(c => c.AnioNacimiento).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.TelefonoMovil1).NotEmpty();
        }
    }

    public class Manejador : IRequestHandler<NewPatientCommand, NewPatientDto>
    {

        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public Manejador(IPatientService pacienteService, IMapper mapper)
        {
            _patientService = pacienteService;
            _mapper = mapper;
        }

        public async Task<NewPatientDto> Handle(NewPatientCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _patientService.NewPatient(_mapper.Map<NewPatientRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<NewPatientDto>(response);

            return result;
        }
    }
}
