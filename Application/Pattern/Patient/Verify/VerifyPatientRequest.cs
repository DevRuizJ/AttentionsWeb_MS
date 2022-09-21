using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.Verify
{
    public class VerifyPatientRequest
    {
        public string TipoDoc { get; set; }
        public string NumeroDoc { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoCasada { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string TelefonoMovil1 { get; set; }
        public string Email { get; set; }
        public string UsuarioModificacion { get; set; } = "ATENCIONESWEB";

    }
}
