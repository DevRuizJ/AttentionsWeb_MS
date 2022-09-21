using Application.Interfaces.Mapping;
using Application.Pattern.Company.Read.Get;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Company.Read.List
{
    public class CompanyListDto : IMapFrom<CompanyListResponse>
    {
        public IEnumerable<CompanyResponse> CompanyList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompanyListResponse, CompanyListDto>();
        }
    }
}
