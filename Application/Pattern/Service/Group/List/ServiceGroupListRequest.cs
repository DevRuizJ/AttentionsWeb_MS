using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Service.Group.List
{
    public class ServiceGroupListRequest : IMapFrom<ServiceGroupListCommand>
    {
        public ServiceGroupListRequest()
        {
            this.Tipo = 1;
        }
        public string IdCompania { get; set; }
        public int Tipo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ServiceGroupListCommand, ServiceGroupListRequest>()
            .ForMember(x => x.IdCompania, y => y.MapFrom(z => z.Compania));
        }
    }
}
