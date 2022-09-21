using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Service.ListXGroup
{
    public class GetServiceListXGroupRequest : IMapFrom<GetServiceListXGroupCommand>
    {
        public string IdCompania { get; set; }
        //public string IdGrupoServicio { get; set; }
        //public int Tipo { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetServiceListXGroupCommand, GetServiceListXGroupRequest>()
            .ForMember(x => x.IdCompania, y => y.MapFrom(z => z.Compania));
        }
    }
}
