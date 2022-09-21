using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace Application.Commom.Helpers
{
    public class Utilities
    {
        public static bool ComprobarFormatoEmail(string seMailAComprobar)
        {
            string sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(seMailAComprobar, sFormato))
            {
                if (Regex.Replace(seMailAComprobar, sFormato, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool ComprobarTelefono(string numero)
        {
            if (numero.Length == 9)
                return true;
            else
                return false;
        }
        public static string EncodeTelefono(string Numero)
        {
            string first = Numero.Substring(0, 1);
            string end = Numero.Substring(Numero.Length - 2, 2);
            return (first + "*****" + end);
        }
        public static string EncodeEmail(string Email)
        {
            string first = Email.Substring(0, 3);
            var end = Email.Split('@');
            return (first + "*****@" + end[1]);
        }

        /// <summary>
        /// Se genera pdf 
        /// </summary>
        /// 
        public static string GuardarPDF(byte[] pdf64, string strPath, string strFile)
        {
            string strFileServer = null;

            if (pdf64 == null) return null;


            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            try
            {
                strFileServer = strPath + @strFile;
                using (FileStream stream = new FileStream(strFileServer, FileMode.Create, FileAccess.Write))
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    long Size = stream.Length + pdf64.Length;
                    for (int i = 0; i < pdf64.Length; i++)
                    {
                        stream.WriteByte(pdf64[i]);
                    }
                    stream.Close();
                }
            }
            catch { }
            return strFileServer;
        }

        /// <summary>
        ///  Elimina el Pdf Creado 
        /// </summary>
        public static void DeletePDF(string strFile)
        {
            try
            {
                System.IO.File.Delete(strFile);
            }
            catch { }
        }

        /// <summary>
        ///  Calcular Edad
        /// </summary>
        public static int GetAge(DateTime fechaNacimiento)
        {
            DateTime now = DateTime.Today;
            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            if (DateTime.Today < fechaNacimiento.AddYears(edad))
                --edad;

            return edad;
        }

        /// <summary>
        ///  Convertir byte[] a Imagen
        /// </summary>
        public static Image ConvertByteArrayToImage(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }

        }

        public static byte[] CreateBarCode(string codigo)
        {
            var barcodeWriterPixelData = new BarcodeWriterPixelData()
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new QrCodeEncodingOptions
                {
                    Height = 100,
                    Width = 250,
                    Margin = 0
                }
            };

            var pixelData = barcodeWriterPixelData.Write(codigo);

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    // PNG or JPEG or whatever you want
                    bitmap.Save(ms, ImageFormat.Png);
                    //var base64str = Convert.ToBase64String(ms.ToArray());

                    return ms.ToArray();

                    //return bitmap;
                    //return base64str;
                }
            }
        }

        public static string ConvertRouteToBase64(string path)
        {
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(path);

                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }

        }
    }
}
