using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.Laboratory;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Laboratory.Samples.Barcode
{

    public class SamplesBarcodeCommand : IRequest<SamplesBarcodeDto>
    {
        public string OSNumber { get; set; }
        public string Period { get; set; }
        public string Year { get; set; }
        public string BranchOffice { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<SamplesBarcodeCommand>
    {
        public ExecuteValidation()
        {

            RuleFor(c => c.OSNumber).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<SamplesBarcodeCommand, SamplesBarcodeDto>
    {
        private readonly ILaboratoryRepository _ticketService;
        private readonly IMapper _mapper;

        public Handler(ILaboratoryRepository ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }
        public async Task<SamplesBarcodeDto> Handle(SamplesBarcodeCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _ticketService.SamplesBarcode(_mapper.Map<SamplesBarcodeRequest>(request));

            if (status != ServiceStatus.Ok)
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );

            var result = _mapper.Map<SamplesBarcodeDto>(response);

            return result;
        }
    }
    
}
