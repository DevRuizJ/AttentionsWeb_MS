using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.BranchOffice.Read.List
{
    public class BranchOfficeListRequest : IMapFrom<BranchOfficeListCommand>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BranchOfficeListCommand, BranchOfficeListRequest>();   
        }
    }
}
