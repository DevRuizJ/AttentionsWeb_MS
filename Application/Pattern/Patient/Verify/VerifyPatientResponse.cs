using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.Verify
{
    public class VerifyPatientResponse
    {
        public string PatientId { get; set; }
        public string PatientYear { get; set; }
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
    }
}
