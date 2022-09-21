using Application.Commom.Status;
using Application.Pattern.Company.Read.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Company
{
    public interface ICompanyRepository
    {
        Task<(ServiceStatus, string, CompanyListResponse)> CompanyList(CompanyListRequest request);
    }
}
