using Application.Interfaces.Mapping;
using Application.Pattern.BranchOffice.Read.Get;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.BranchOffice.Read.List
{
    public class BranchOfficeListDto : IMapFrom<BranchOfficeListResponse>
    {
        public IEnumerable<BranchOfficeResponse> BranchOfficeList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BranchOfficeListResponse, BranchOfficeListDto>();
        }
    }
}
