using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Read.List
{
    public class AttentionListRequest : IMapFrom<AttentionListCommand>
    {
        public string Numcia { get; set; }
        public string Numsuc { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AttentionListCommand, AttentionListRequest>()
                .ForMember(a => a.Numcia, opt => opt.MapFrom(src => src.Compania))
                .ForMember(a => a.Numsuc, opt => opt.MapFrom(src => src.Sucursal));
        }
    }
}
