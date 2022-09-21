using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.Get
{
    public class GetPatientDto : IMapFrom<GetPatientResponse>
    {
        public GetPatientDto()
        {
            this.Edad = 0;
        }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Paciente { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Idpaciente { get; set; }
        public string AnioPaciente { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetPatientResponse, GetPatientDto>()
            .ForMember(dest => dest.Paciente, opt => opt.MapFrom(
                src => src.Sexo == "F" ? src.ApellidoPaterno + " " + src.ApellidoMaterno + " " + src.ApellidoCasada + " " + src.Nombre : src.ApellidoPaterno + " " + src.ApellidoMaterno + " " + src.Nombre))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.TelefonoMovil1))
            .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Edad != "" ? int.Parse(src.Edad) : 0))
            .ForMember(dest => dest.Idpaciente, opt => opt.MapFrom(src => src.Numpac))
            .ForMember(dest => dest.AnioPaciente, opt => opt.MapFrom(src => src.AnioPac));
        }
    }
}
