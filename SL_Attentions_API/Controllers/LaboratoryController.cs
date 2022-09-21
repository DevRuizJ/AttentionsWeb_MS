using Application.Pattern.Laboratory.Samples.Barcode;
using Microsoft.AspNetCore.Mvc;

namespace SL_Attentions_API.Controllers
{
    public class LaboratoryController : BaseController
    {
        [HttpPost("barcode-to-samples")]
        public async Task<ActionResult<SamplesBarcodeDto>> BarcodeToSamples(SamplesBarcodeCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }
    }
}
