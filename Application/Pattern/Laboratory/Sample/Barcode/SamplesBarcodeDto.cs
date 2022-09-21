using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Laboratory.Samples.Barcode
{
    public class SamplesBarcodeDto : IMapFrom<SamplesBarcodeResponse>
    {
        public byte[] SamplesBarcode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SamplesBarcodeResponse, SamplesBarcodeDto>()
            .ForMember(b => b.SamplesBarcode, opt => opt.MapFrom(src => src.pdf));
        }
    }
}
