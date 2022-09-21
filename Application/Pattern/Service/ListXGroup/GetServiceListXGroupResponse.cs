using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Service.ListXGroup
{
    public class GetServiceListXGroupResponse
    {
        public string Numser { get; set; }
        public string Seekcia { get; set; }
        public string Descripcion { get; set; }
        public decimal TotalDol { get; set; }
        public decimal TotalSol { get; set; }
        public decimal Precio { get; set; }
    }
}
