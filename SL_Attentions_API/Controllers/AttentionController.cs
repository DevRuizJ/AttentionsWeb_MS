using Application.Pattern.Attention.Create.New;
using Application.Pattern.Attention.Create.Ticket;
using Application.Pattern.Attention.Read.List;
using Microsoft.AspNetCore.Mvc;

namespace SL_Attentions_API.Controllers
{
    public class AttentionController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<NewAttentionDto>> NewAttention(NewAttentionCommand parametros)
        {
            var response = await Mediator.Send(parametros);
            return Ok(response);
        }

        [HttpPost("list")]
        public async Task<ActionResult<AttentionListDto>> AttentionList(AttentionListCommand data)
        {
            var response = await Mediator.Send(data);
            return Ok(response);
        }

        [HttpPost("ticket")]
        public async Task<ActionResult<AttentionTicketDto>> AttentionTicket(AttentionTicketCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }       
    }
}
