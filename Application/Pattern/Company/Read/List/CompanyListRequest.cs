using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Company.Read.List
{
    public class CompanyListRequest : IMapFrom<CompanyListCommand>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompanyListCommand, CompanyListRequest>();
        }
    }
}
