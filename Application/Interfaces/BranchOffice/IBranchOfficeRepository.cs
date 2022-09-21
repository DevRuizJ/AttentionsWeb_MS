using Application.Commom.Status;
using Application.Pattern.BranchOffice.Read.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.BranchOffice
{
    public interface IBranchOfficeRepository
    {
        Task<(ServiceStatus, string, BranchOfficeListResponse)> BranchOfficeList(BranchOfficeListRequest request);
    }
}
