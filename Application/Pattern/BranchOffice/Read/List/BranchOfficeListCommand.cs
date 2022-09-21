using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.BranchOffice;
using Application.Interfaces.Mapping;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.BranchOffice.Read.List
{
    public class BranchOfficeListCommand : IRequest<BranchOfficeListDto>
    {
        //PARAMETROS
    }

    public class ExecuteValidation : AbstractValidator<BranchOfficeListCommand>
    {
        public ExecuteValidation()
        {
            // Validate Fluent Validation
        }
    }

    public class Handler : IRequestHandler<BranchOfficeListCommand, BranchOfficeListDto>
    {
        private readonly IBranchOfficeRepository _branchOfficeRepository;
        private readonly IMapper _mapper;

        public Handler(IBranchOfficeRepository branchOfficeRepository, IMapper mapper)
        {
            _branchOfficeRepository = branchOfficeRepository;
            _mapper = mapper;
        }

        public async Task<BranchOfficeListDto> Handle(BranchOfficeListCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _branchOfficeRepository.BranchOfficeList(_mapper.Map<BranchOfficeListRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result = _mapper.Map<BranchOfficeListDto>(response);

            return result;
        }
    }
}
