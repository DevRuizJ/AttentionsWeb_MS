using Application.Interfaces.Mapping;
using Application.Pattern.Service.Read.Get;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Service.Read.Filter
{
    public class ServiceFilterDto : IMapFrom<ServiceFilterResponse>
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ServiceFilterResponse, ServiceFilterDto>()
            .ForMember(opt => opt.Id, dest => dest.MapFrom(src => src.Seekcia))
            .ForMember(opt => opt.Codigo, dest => dest.MapFrom(src => src.Numser))
            .ForMember(opt => opt.Descripcion, dest => dest.MapFrom(src => src.Descripcion))
            .ForMember(opt => opt.Precio, dest => dest.MapFrom(src => src.Precio));
        }
    }
}
