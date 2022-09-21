using Application.Interfaces.Mapping;
using Application.Pattern.Attention.Read.Get;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Read.List
{
    public class AttentionListDto : IMapFrom<AttentionListResponse>
    {
        public IEnumerable<AttentionResponse> attentionList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AttentionListResponse, AttentionListDto>();
        }
    }
}
