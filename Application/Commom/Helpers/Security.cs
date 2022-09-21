using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commom.Helpers
{
    public class Security
    {
        /// <summary>
        /// Encriptación Suiza Lab
        /// </summary>
        public static string Encrypt(string sPassword)
        {
            string sEncriptado = string.Empty;
            int iContador = sPassword.Length;

            int[] aux = new int[] { 3, 24, 8, 10, 34, 17, 20, 21, 21, 3, 24, 8, 10, 34, 17, 20 };

            for (int i = 0; i < iContador; i++)
            {
                sEncriptado = (sEncriptado + Convert.ToChar(Encoding.ASCII.GetBytes(sPassword.Substring(i, 1))[0] + aux[i]).ToString());
            }

            return sEncriptado;
        }

        /// <summary>
        /// Desencriptación Suiza Lab
        /// </summary>
        public static string Decrypt(string sPassword)
        {
            string sDesencriptado = string.Empty;
            int iContador = sPassword.Length;

            int[] aux = new int[] { 3, 24, 8, 10, 34, 17, 20, 21, 21, 3, 24, 8, 10, 34, 17, 20 };

            for (int i = 0; i < iContador; i++)
            {
                sDesencriptado = (sDesencriptado + Convert.ToChar(Encoding.ASCII.GetBytes(sPassword.Substring(i, 1))[0] - aux[i]).ToString());
            }

            return sDesencriptado;
        }
    }
}
