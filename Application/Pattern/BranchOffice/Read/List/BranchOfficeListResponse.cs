using Application.Pattern.BranchOffice.Read.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.BranchOffice.Read.List
{
    public class BranchOfficeListResponse
    {
        public List<BranchOfficeResponse> BranchOfficeList { get; set; }
    }
}
