using Application.Pattern.Company.Read.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Company.Read.List
{
    public class CompanyListResponse
    {
        public List<CompanyResponse> CompanyList { get; set; }
    }
}
