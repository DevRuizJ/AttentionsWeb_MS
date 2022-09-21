using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Service.Read.Filter
{
    public class ServiceFilterRequest : IMapFrom<ServiceFilterCommand>
    {
        public string IdCompania { get; set; }
        public int Tipo { get; set; } = 1;
        public string Filtro { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ServiceFilterCommand, ServiceFilterRequest>()
            .ForMember(x => x.IdCompania, y => y.MapFrom(z => z.Compania))
            .ForMember(x => x.Filtro, y => y.MapFrom(z => z.Filtro));
        }
    }
}
