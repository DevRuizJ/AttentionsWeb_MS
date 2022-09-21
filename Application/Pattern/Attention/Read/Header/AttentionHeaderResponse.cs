using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Read.Header
{
    public class AttentionHeaderResponse
    {
        public string Ticket { get; set; }
        public string Numoscab { get; set; }
        public string Peroscab { get; set; }
        public string Anooscab { get; set; }
        public string Numsuc { get; set; }
        public string Numemp { get; set; }
        public string Idpaciente { get; set; }
        public string Aniopaciente { get; set; }
        public string Paciente { get; set; }
        public string TipoDoc { get; set; }
        public string PacienteDoc { get; set; }
        public string FechaAtencion { get; set; }
        public string HoraAtencion { get; set; }
        public decimal TotalSol { get; set; }
        public decimal TotalDol { get; set; }
    }
}
