using Application.Commom.Status;
using Application.Pattern.Patient.Get;
using Application.Pattern.Patient.New;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Patient
{
    public interface IPatientService
    {
        Task<(ServiceStatus, string, GetPatientResponse)> GetPatient(GetPatientRequest request);
        Task<(ServiceStatus, string, GetPatientResponse)> NewPatient(NewPatientRequest request);
    }
}
