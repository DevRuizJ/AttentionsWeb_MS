using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Company.Read.Get
{
    public class CompanyResponse
    {
        public string IdCompania { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string UsuarioWeb { get; set; }
        public string Seekcia { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }

        //Parametros para la notificación a los pacientes de la referencia
        public string Logo { get; set; }

        //Datos de Facturación
        public string Direccion { get; set; }
        public string Moneda { get; set; }
        public string codFormaPago { get; set; }
        public string FormaPago { get; set; }
        public string TipoDocumentoPago { get; set; }
        public string TipoDocumentoPagoDesc { get; set; }
    }
}
