using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.ValidateUser
{
    public class ValidateUserResponse
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Usuario { get; set; }
        public Int16 ValUser { get; set; }
        public Int16 ValPassword { get; set; }
    }
}
