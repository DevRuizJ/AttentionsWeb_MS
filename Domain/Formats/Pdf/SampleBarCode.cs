using Application.Pattern.Laboratory.Sample.Read.List;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Formats.Pdf
{
    public class SampleBarCode
    {
        public static byte[] GetPDFSampleBarCodeByteArray(SampleListResponse data)
        {
            var pgSize = new Rectangle(2600, 1500);
            Document pdfDoc = new Document(pgSize, 500f, 200f, 600f, 0);

            Font _principalFont = new Font(Font.FontFamily.COURIER, 70, Font.NORMAL, BaseColor.BLACK);
            Font _codeFont = new Font(Font.FontFamily.HELVETICA, 85, Font.NORMAL, BaseColor.BLACK);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 55, Font.NORMAL, BaseColor.BLACK);

            PdfPCell cell;

            PdfPTable pdfWorkSpace = new PdfPTable(15);
            //pdfWorkSpace.WidthPercentage = 100;

            foreach (var sample in data.Samples)
            {
                #region FORMATO 1

                //FILA PRINCIPAL
                //Columna 1
                cell = new PdfPCell(new Phrase(sample.Tubo, _principalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.Border = 0;
                cell.Rotation = 90;
                pdfWorkSpace.AddCell(cell);
                //Columna 2
                PdfPTable tbLabo = new PdfPTable(20);
                //Columna 2 Fila 1 ---------------------------------------------------------------------------------
                cell = new PdfPCell(new Phrase(data.Patient, _principalFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 20;
                cell.Border = 0;
                cell.PaddingBottom = 25;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 2 ---------------------------------------------------------------------------------
                System.Drawing.Image sampleBarcode = Application.Commom.Helpers.Utilities.ConvertByteArrayToImage(sample.BarCode);
                Image imgSampleBarcode = Image.GetInstance(sampleBarcode, System.Drawing.Imaging.ImageFormat.Png);
                imgSampleBarcode.SetAbsolutePosition(0, 0);
                imgSampleBarcode.ScaleAbsoluteHeight(pdfDoc.PageSize.Height / 4);
                imgSampleBarcode.ScaleAbsoluteWidth(pdfDoc.PageSize.Width / 2);
                cell = new PdfPCell(imgSampleBarcode);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 20;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                // Columna 2 Fila 3 ---------------------------------------------------------------------------------
                cell = new PdfPCell(new Phrase(sample.Codigo, _codeFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 20;
                cell.Border = 0;
                cell.PaddingBottom = 25;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 4 ---------------------------------------------------------------------------------
                cell = new PdfPCell(new Phrase(sample.Descripcion, _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 20;
                cell.Border = 0;
                cell.PaddingBottom = 25;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 ---------------------------------------------------------------------------------
                //Columna 2 Fila 5 Columna 1
                cell = new PdfPCell(new Phrase("Sexo", _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 2;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 2
                cell = new PdfPCell(new Phrase(":", _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 3
                cell = new PdfPCell(new Phrase(data.Sex, _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 5;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 4
                cell = new PdfPCell(new Phrase("-", _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 5
                cell = new PdfPCell(new Phrase("Edad", _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 3;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 6
                cell = new PdfPCell(new Phrase(":", _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 7
                cell = new PdfPCell(new Phrase(data.Age, _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 2;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 8
                cell = new PdfPCell(new Phrase("-", _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //Columna 2 Fila 5 Columna 9
                cell = new PdfPCell(new Phrase("SUIZALAB", _standardFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                cell.Border = 0;
                tbLabo.AddCell(cell);
                //-----------------------------------------------------------------------------------------------------------
                cell = new PdfPCell(tbLabo);
                cell.Colspan = 14;
                cell.BorderWidth = 0;
                cell.PaddingBottom = 15;
                pdfWorkSpace.AddCell(cell);

                #endregion FORMATO 1             
            }

            MemoryStream memoryStream = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

            pdfDoc.Open();

            //PdfContentByte cb = writer.DirectContent;
            //cb.MoveTo(0, pdfDoc.PageSize.Height / 2);
            //cb.LineTo(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height / 2);
            //cb.Stroke();

            pdfDoc.Add(pdfWorkSpace);

            pdfDoc.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            return bytes;
        }
    }
}
