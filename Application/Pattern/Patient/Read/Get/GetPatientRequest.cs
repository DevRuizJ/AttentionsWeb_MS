using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Patient.Get
{
    public class GetPatientRequest : IMapFrom<GetPatientCommand>
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetPatientCommand, GetPatientRequest>()
                .ForMember(p => p.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento.Substring(2, 1)));
        }
    }
}
