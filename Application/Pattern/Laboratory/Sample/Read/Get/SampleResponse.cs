using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Laboratory.Sample.Read.Get
{
    public class SampleResponse
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Nroorden { get; set; }
        public string FechaEscaneo { get; set; }
        public string Tubo { get; set; }
        public byte[] BarCode { get; set; }
    }
}
