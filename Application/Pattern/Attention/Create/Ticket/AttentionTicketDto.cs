using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Create.Ticket
{
    public class AttentionTicketDto : IMapFrom<AttentionTicketResponse>
    {
        public byte[] TicketPdf { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AttentionTicketResponse, AttentionTicketDto>()
                .ForMember(t => t.TicketPdf, opt => opt.MapFrom(src => src.pdf));
        }
    }
}
