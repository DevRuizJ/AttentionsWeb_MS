using Application.Pattern.Laboratory.Sample.Read.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Laboratory.Sample.Read.List
{
    public class SampleListResponse
    {
        public string Patient { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public List<SampleResponse> Samples { get; set; }
        public byte[] SamplesBarcode { get; set; }
    }
}
