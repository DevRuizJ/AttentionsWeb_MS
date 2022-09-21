using Application.Commom.Status;
using Application.Pattern.Service.Group.List;
using Application.Pattern.Service.ListXGroup;
using Application.Pattern.Service.Read.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IServiceRepository
    {
        Task<(ServiceStatus, string, IEnumerable<ServiceGroupListResponse>)> GetServiceGroupList(ServiceGroupListRequest request);
        Task<(ServiceStatus, string, IEnumerable<GetServiceListXGroupResponse>)> GetServiceListXGroup(GetServiceListXGroupRequest request);
        Task<(ServiceStatus, string, IEnumerable<ServiceFilterResponse>)> ServiceFilter(ServiceFilterRequest request);
    }
}
