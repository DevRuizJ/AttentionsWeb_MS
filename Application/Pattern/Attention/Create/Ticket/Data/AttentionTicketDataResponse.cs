using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Create.Ticket.Data
{

    public class AttentionTicketDataResponse
    {
        public AttentionNavalHead Head;

        public List<AttentionNavalBody> Body;
    }

    public class AttentionNavalHead
    {
        [DataMember(Name = "barcode")]
        public byte[] Barcode { get; set; }

        [DataMember(Name = "empresa")]
        public string Empresa { get; set; }

        [DataMember(Name = "sucursal")]
        public string Sucursal { get; set; }

        [DataMember(Name = "fecha")]
        public DateTime Fecha { get; set; }

        [DataMember(Name = "usuario")]
        public string Usuario { get; set; }

        [DataMember(Name = "ticket")]
        public string Ticket { get; set; }

        [DataMember(Name = "orblab")]
        public string OrdLab { get; set; }

        [DataMember(Name = "compania")]
        public string Compania { get; set; }

        [DataMember(Name = "paciente")]
        public string Paciente { get; set; }

        [DataMember(Name = "dx")]
        public string Dx { get; set; }

        [DataMember(Name = "document")]
        public string Document { get; set; }

        [DataMember(Name = "cid")]
        public string Cip { get; set; }

        [DataMember(Name = "sexo")]
        public string Sexo { get; set; }

        [DataMember(Name = "fecnac")]
        public string Fecnac { get; set; }

        [DataMember(Name = "edad")]
        public string Edad { get; set; }

        [DataMember(Name = "telefono")]
        public string Telefono { get; set; }

        [DataMember(Name = "piso")]
        public string Piso { get; set; }

        [DataMember(Name = "observacion")]
        public string Observacion { get; set; }

        [DataMember(Name = "paren")]
        public string Paren { get; set; }

        [DataMember(Name = "usuario_web")]
        public string UsuarioWeb { get; set; }

        [DataMember(Name = "contrasena")]
        public string Contrasena { get; set; }
    }

    public class AttentionNavalBody
    {
        [DataMember(Name = "cantidad")]
        public string Cantidad { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "check")]
        public string Check { get; set; }
    }
    
}
