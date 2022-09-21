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

namespace Application.Pattern.Attention.Create.New
{
    public class NewAttentionCommand : IRequest<NewAttentionDto>
    {
        public string AnioPaciente { get; set; }
        public string NumeroPaciente { get; set; }
        public string Compania { get; set; }
        public string RegistrationUser { get; set; }
        public string Telefono1 { get; set; }
        public string correo { get; set; }
        public string Servicios { get; set; }
        public string BranchOffice { get; set; }
        public string Observation { get; set; }
        public string Discount { get; set; }

        //TARIFA CUBIERTA POR NAVAL
        //public string TarifaIpress { get; set; }    //IAFAS
        //public string TarifaDisamar { get; set; }   //DISAMAR   ----------------------
        //public string Diagnosis { get; set; }
        //public string NavalCareUnit { get; set; }        
    }

    public class ExecuteValidation : AbstractValidator<NewAttentionCommand>
    {
        public ExecuteValidation()
        {
            // Validate Fluent Validation
            RuleFor(c => c.Compania).NotEmpty().Length(7);
            RuleFor(c => c.AnioPaciente).NotEmpty().Length(2).WithMessage("Año del registro de paciente.");
            RuleFor(c => c.NumeroPaciente).NotEmpty().Length(7).WithMessage("Id del paciente");
            RuleFor(c => c.Servicios).NotEmpty().MinimumLength(7).WithMessage("Los servicios deben de estar separados por comas.");
        }
    }

    public class Handler : IRequestHandler<NewAttentionCommand, NewAttentionDto>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionRepository _attentionRepository;

        public Handler(IMapper mapper, IAttentionRepository attentionRepository)
        {
            _mapper = mapper;
            _attentionRepository = attentionRepository;
        }

        public async Task<NewAttentionDto> Handle(NewAttentionCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _attentionRepository.NewAttention(_mapper.Map<NewAttentionRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<NewAttentionDto>(response);

            return result;
        }
    }
}
