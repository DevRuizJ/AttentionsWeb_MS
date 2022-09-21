using Application.Pattern.BranchOffice.Read.List;
using Microsoft.AspNetCore.Mvc;

namespace SL_Attentions_API.Controllers
{
    public class BranchOfficeController : BaseController
    {
        [HttpPost("list")]
        public async Task<ActionResult<BranchOfficeListDto>> BranchOfficeList(BranchOfficeListCommand parametros)
        {
            var response = await Mediator.Send(parametros);
            return Ok(response);
        }
    }
}
