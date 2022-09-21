using Application.Pattern.Service.Group.List;
using Application.Pattern.Service.ListXGroup;
using Application.Pattern.Service.Read.Filter;
using Microsoft.AspNetCore.Mvc;

namespace SL_Attentions_API.Controllers
{
    public class ServiceController : BaseController
    {
        //[HttpPost("grupos")]
        //public async Task<ActionResult<IEnumerable<ServiceGroupListDto>>> GetServiceGroupList(ServiceGroupListCommand data)
        //{
        //    var response = await Mediator.Send(data);
        //    return Ok(response);
        //}

        [HttpPost("all")]
        public async Task<ActionResult<IEnumerable<GetServiceListXGroupDto>>> GetServiceListXGroup(GetServiceListXGroupCommand data)
        {
            var response = await Mediator.Send(data);
            return Ok(response);
        }

        [HttpPost("filter-data")]
        public async Task<ActionResult<IEnumerable<ServiceFilterDto>>> ServiceFilter(ServiceFilterCommand data)
        {
            var response = await Mediator.Send(data);

            return Ok(response);
        }

    }
}
