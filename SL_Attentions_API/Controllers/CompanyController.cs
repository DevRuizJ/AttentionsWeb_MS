using Application.Pattern.Company.Read.List;
using Microsoft.AspNetCore.Mvc;

namespace SL_Attentions_API.Controllers
{
    public class CompanyController : BaseController
    {
        [HttpPost("list")]
        public async Task<ActionResult<CompanyListDto>> CompanyList(CompanyListCommand parametros)
        {
            var response = await Mediator.Send(parametros);
            return Ok(response);
        }
    }
}
