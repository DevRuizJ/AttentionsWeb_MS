using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.PatientService
{
    public class Endpoints
    {
        public class Patient
        {
            private const string PathEndpoint = "/api/patient";
            //public static string Get => "/api/paciente";
            public static string Get => "/api/patient";
            public static string New => $"{PathEndpoint}/create";
        }
    }
}
