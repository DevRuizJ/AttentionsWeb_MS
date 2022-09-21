using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Create.Ticket
{
    public class AttentionTicketRequest : IMapFrom<AttentionTicketCommand>
    {
        public string Numoscab { get; set; }
        public string Peroscab { get; set; }
        public string Anioscab { get; set; }
        public string Numsuc { get; set; }
        public string Company { get; set; }
        public string Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AttentionTicketCommand, AttentionTicketRequest>()
                .ForMember(x => x.Numoscab, y => y.MapFrom(z => z.OSNumber))
                .ForMember(x => x.Peroscab, y => y.MapFrom(z => z.Period))
                .ForMember(x => x.Anioscab, y => y.MapFrom(z => z.Year))
                .ForMember(x => x.Numsuc, y => y.MapFrom(z => z.BranchOffice));
        }
    }
}
