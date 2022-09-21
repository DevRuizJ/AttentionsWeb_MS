using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.Get
{
    public class GetPatientResponse
    {
        [JsonPropertyName("tipoDocumento")]
        public string TipoDocumento { get; set; }

        [JsonPropertyName("numeroDocumento")]           //api/paciente
        public string NumeroDocumento { get; set; }

        [JsonPropertyName("nombre")]                    //api/paciente
        public string Nombre { get; set; }

        [JsonPropertyName("apellidoPaterno")]           //api/paciente
        public string ApellidoPaterno { get; set; }

        [JsonPropertyName("apellidoMaterno")]           //api/paciente
        public string ApellidoMaterno { get; set; }

        [JsonPropertyName("apellidoCasada")]            //api/paciente
        public string ApellidoCasada { get; set; }

        [JsonPropertyName("fechaNacimiento")]           //api/paciente
        public string FechaNacimiento { get; set; }

        [JsonPropertyName("estadoCivil")]               //api/paciente
        public string EstadoCivil { get; set; }

        [JsonPropertyName("sexo")]                      //api/paciente
        public string Sexo { get; set; }

        [JsonPropertyName("telefonoMovil1")]            //api/paciente
        public string TelefonoMovil1 { get; set; }

        [JsonPropertyName("telefonoMovil2")]            //api/paciente
        public string TelefonoMovil2 { get; set; }

        [JsonPropertyName("email")]                     //api/paciente
        public string Email { get; set; }

        [JsonPropertyName("edad")]                      //api/paciente
        public string Edad { get; set; }

        [JsonPropertyName("numPac")]                    //api/paciente
        public string Numpac { get; set; }

        [JsonPropertyName("anioPac")]                   //api/paciente
        public string AnioPac { get; set; }
        public string Mensaje { get; set; }
    }
}
