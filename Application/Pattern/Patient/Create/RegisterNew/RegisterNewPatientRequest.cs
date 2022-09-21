using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.RegisterNew
{
    public class RegisterNewPatientRequest : IMapFrom<RegisterNewPatientCommand>
    {
        public RegisterNewPatientRequest()
        {
            this.CodDistrito = "00";
            this.CodProvincia = "00";
            this.CodDepartamento = "00";
            this.TelefonoMovil1 = "-";
            this.Direccion = "-";
        }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string TelefonoMovil1 { get; set; }
        public string CodDepartamento { get; set; }
        public string CodProvincia { get; set; }
        public string CodDistrito { get; set; }
        public string UsuarioRegistro { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterNewPatientCommand, RegisterNewPatientRequest>()
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento))
            .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.NumeroDocumento))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.ApellidoPaterno, opt => opt.MapFrom(src => src.ApellidoPaterno))
            .ForMember(dest => dest.ApellidoMaterno, opt => opt.MapFrom(src => src.ApellidoMaterno))
            .ForMember(dest => dest.Sexo, opt => opt.MapFrom(src => src.Sexo))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento));
            ;
        }
    }
}
