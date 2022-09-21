using Application.Commom.Middleware;
using Application.Commom.Status;
using Application.Interfaces.Company;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Company.Read.List
{
    public class CompanyListCommand : IRequest<CompanyListDto>
    {
        //PARAMETROS
    }

    public class ExecuteValidation : AbstractValidator<CompanyListCommand>
    {
        public ExecuteValidation()
        {
            //SIN PARAMETROS
        }
    }

    public class Handler : IRequestHandler<CompanyListCommand, CompanyListDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public Handler(IMapper mapper, ICompanyRepository companiaRepository)
        {
            _mapper = mapper;
            _companyRepository = companiaRepository;
        }

        public async Task<CompanyListDto> Handle(CompanyListCommand request, CancellationToken cancellationToken)
        {
            var (status, message, response) = await _companyRepository.CompanyList(_mapper.Map<CompanyListRequest>(request));

            if (status != ServiceStatus.Ok)
            {
                throw new ErrorHandler(
                        status == ServiceStatus.FailedValidation
                        ? HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError
                    , message
                    );
            }

            var result =  _mapper.Map<CompanyListDto>(response);

            return result;
        }
    }
}
