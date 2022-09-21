using Application.Pattern.Attention.Read.Detail;
using Application.Pattern.Attention.Read.Header;
using Application.Pattern.Service.Read.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Read.Get
{
    public class AttentionResponse
    {
        public AttentionHeaderResponse Header { get; set; }
        public AttentionDetailResponse Detail { get; set; }        

        ////TARIFA DE COBERTURA NAVAL
        //public string Cobiafas { get; set; }        //IAFAS
        //public string Cobdisamar { get; set; }      //DISAMAR
        //public decimal TotalNaval { get; set; }
    }
}
