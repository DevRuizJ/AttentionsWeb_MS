using Application.Interfaces.Mapping;
using Application.Pattern.Patient.Get;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.New
{
    public class NewPatientDto : IMapFrom<GetPatientResponse>
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoCasada { get; set; }
        public string FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public string Sexo { get; set; }   
        public string TelefonoMovil1 { get; set; }
        public string TelefonoMovil2 { get; set; }
        public string Email { get; set; }
        public string Edad { get; set; }
        public string Nacionalidad { get; set; }
        public string Direccion { get; set; }
        public string Message { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetPatientResponse, NewPatientDto>()
                .ForMember(p => p.Message, opt => opt.MapFrom(src => src.Mensaje));

        }
    }
}
