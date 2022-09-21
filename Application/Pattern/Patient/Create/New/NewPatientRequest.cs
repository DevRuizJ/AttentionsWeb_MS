using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.New
{
    public class NewPatientRequest : IMapFrom<NewPatientCommand>
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string TelefonoMovil1 { get; set; }
        public string ApellidoCasada { get; set; }
        public string EstadoCivil { get; set; }
        public string Direccion { get; set; }
        public string TelefonoMovil2 { get; set; }
        public string Nacionalidad { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPatientCommand, NewPatientRequest>()
                .ForMember(f => f.FechaNacimiento, opt => opt.MapFrom(src => src.AnioNacimiento + "/" + src.MesNacimiento + "/" + src.DiaNacimiento));
        }
    }
}
