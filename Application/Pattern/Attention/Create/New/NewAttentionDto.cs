using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Create.New
{
    public class NewAttentionDto : IMapFrom<NewAttentionResponse>
    {
        public string Message { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewAttentionResponse, NewAttentionDto>();
        }
    }
}
