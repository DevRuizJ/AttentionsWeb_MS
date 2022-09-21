using Application.Commom.Status;
using Application.Pattern.Patient.Get;
using Application.Pattern.Patient.Verify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Patient
{
    public interface IPatientRepository
    {
        Task<(ServiceStatus, string, VerifyPatientResponse)> VerifyPatient(VerifyPatientRequest request);
    }
}
