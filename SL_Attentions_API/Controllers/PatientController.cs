using Application.Pattern.Patient.Get;
using Application.Pattern.Patient.New;
using Microsoft.AspNetCore.Mvc;

namespace SL_Attentions_API.Controllers
{
    public class PatientController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<GetPatientDto>> GetPatient(GetPatientCommand parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpPost("new-record")]
        public async Task<ActionResult<NewPatientDto>> NewPatient(NewPatientCommand parametros)
        {
            return await Mediator.Send(parametros);
        }
    }
}
