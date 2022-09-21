using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Service.Group.List
{
    public class ServiceGroupListDto : IMapFrom<ServiceGroupListResponse>
    {
        public string IdGrupo { get; set; }
        public string Descripcion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ServiceGroupListResponse, ServiceGroupListDto>()
            .ForMember(opt => opt.IdGrupo, dest => dest.MapFrom(src => src.Codigo))
            .ForMember(opt => opt.Descripcion, dest => dest.MapFrom(src => src.Descripcion));
        }
    }
}
