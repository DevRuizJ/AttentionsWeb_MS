using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Laboratory.Samples.Barcode
{
    public class SamplesBarcodeRequest : IMapFrom<SamplesBarcodeCommand>
    {
        public string OSNumber { get; set; }
        public string Period { get; set; }
        public string Year { get; set; }
        public string BranchOffice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SamplesBarcodeCommand, SamplesBarcodeRequest>();
        }
    }
}
