using Application.Pattern.Attention.Create.Ticket.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Formats.Pdf
{
    public class GetAttentionTicket
    {
        public static byte[] GetPDFTicketByteArray(AttentionTicketDataResponse data)
        {
            var pgSize = new Rectangle(2000, 10000);
            Document pdfDoc = new Document(pgSize);
            PdfPTable pdfWorkSpace = new PdfPTable(50);
            pdfWorkSpace.WidthPercentage = 100;

            Font _titleFont = new Font(Font.FontFamily.HELVETICA, 65, Font.BOLD, BaseColor.BLACK);
            Font _subTitleFont = new Font(Font.FontFamily.HELVETICA, 63, Font.BOLD, BaseColor.BLACK);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 60, Font.BOLD, BaseColor.BLACK);

            PdfPCell cell;

            var txtAlign = Element.ALIGN_LEFT;
            var dataAlign = Element.ALIGN_LEFT;
            string twoPoints = ":";
            Chunk linebreak = new Chunk(new LineSeparator(10f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
            string arrow = "==>";

            #region ESTRUCTURA TICKET

            #region DATOS DE LA ATENCION         
            #region FILA 1
            //Columna1 
            cell = new PdfPCell(new Phrase(data.Head.Empresa, _titleFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 25;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);

            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Sucursal, _titleFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 25;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 1

            #region FILA 2
            //Columa 1 ---------------------------------------------------------------------------------30
            string txtFechaAtencion = "Fec. Atención";
            cell = new PdfPCell(new Phrase(txtFechaAtencion, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 11;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columa 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columa 3
            cell = new PdfPCell(new Phrase(data.Head.Fecha.ToString("dd/MM/yyyy HH:mm tt"), _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 18;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columa 4 ---------------------------------------------------------------------------------20
            string txtUsuario = "User";
            cell = new PdfPCell(new Phrase(txtUsuario, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 4;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columa 5
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columa 6
            cell = new PdfPCell(new Phrase(data.Head.Usuario, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 15;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 2

            #region FILA 3
            //Columna 1 ---------------------------------------------------------------------------------30
            string txtNroTicket = "Nro. Ticket";
            cell = new PdfPCell(new Phrase(txtNroTicket, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 11;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 3
            cell = new PdfPCell(new Phrase(data.Head.Ticket, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 18;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 4 ---------------------------------------------------------------------------------20
            string txtOrdLab = "Ord. Lab.";
            cell = new PdfPCell(new Phrase(txtOrdLab, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 8;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 5
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 6
            cell = new PdfPCell(new Phrase(data.Head.OrdLab, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 11;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 3
            #endregion

            #region FILA 4 CODIGO DE BARRAS
            //Columa 1
            System.Drawing.Image ticketBarCode = Application.Commom.Helpers.Utilities.ConvertByteArrayToImage(data.Head.Barcode);
            Image imgTicketBarCode = Image.GetInstance(ticketBarCode, System.Drawing.Imaging.ImageFormat.Png);
            imgTicketBarCode.SetAbsolutePosition(0, 0);
            imgTicketBarCode.ScaleAbsoluteHeight(pdfDoc.PageSize.Height / 35);
            imgTicketBarCode.ScaleAbsoluteWidth(pdfDoc.PageSize.Width / 1.2f);
            cell = new PdfPCell(imgTicketBarCode);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 4

            #region LINEA
            cell = new PdfPCell(new Phrase(linebreak));
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion

            #region DATOS AFILIADO
            #region FILA 5
            //Columna 1
            string txtCompania = "Compañía";
            cell = new PdfPCell(new Phrase(txtCompania, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 9;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columa 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 3
            cell = new PdfPCell(new Phrase(data.Head.Compania, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 40;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 5

            #region FILA 6 
            //Columna 1 ---------------------------------------------------------------------------------50
            string txtPaciente = "Paciente";
            cell = new PdfPCell(new Phrase(txtPaciente, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 7;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 3
            cell = new PdfPCell(new Phrase(data.Head.Paciente, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 42;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #region Columna 4 -------------------------------------------------------------------------------- "-7" ---NO USADO
            /*
            string txtDX = "DX";
            cell = new PdfPCell(new Phrase(txtDX, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 3;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 5
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 6
            cell = new PdfPCell(new Phrase("TTT", _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 3;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            */
            #endregion
            #endregion FILA 6

            #region FILA 7
            //Columna 2 ---------------------------------------------------------------------------------31
            string txtDocumento = "Docum.";
            cell = new PdfPCell(new Phrase(txtDocumento, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 7;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Document, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 23;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #region Columna 2 --------------------------------------------------------------------------------"-16" ---NO USADO
            /*
            string txtCIP = "CIP";
            cell = new PdfPCell(new Phrase(txtCIP, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 3;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Cip, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 12;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            */
            #endregion
            //Columna 2 ---------------------------------------------------------------------------------19
            string txtSexo = "Sexo";
            cell = new PdfPCell(new Phrase(txtSexo, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 7;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Sexo, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 11;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 7

            #region FILA 8
            //Columna 1 ---------------------------------------------------------------------------------19
            string txtFecNac = "F. Nac.";
            cell = new PdfPCell(new Phrase(txtFecNac, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 6;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columa 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Fecnac, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 12;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 3 ---------------------------------------------------------------------------------11
            string txtEdad = "Edad";
            cell = new PdfPCell(new Phrase(txtEdad, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 5;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columa 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Edad, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 5;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 3 ---------------------------------------------------------------------------------20
            string txtTelef = "Telef.";
            cell = new PdfPCell(new Phrase(txtTelef, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 5;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columa 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Telefono, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 14;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #region Columna 3 --------------------------------------------------------------------------------"-12" ---NO USADO
            /*
            string txtParen = "Paren.";
            cell = new PdfPCell(new Phrase(txtParen, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 6;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columa 2
            cell = new PdfPCell(new Phrase(twoPoints, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 1;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(data.Head.Paren, _standardFont));
            cell.HorizontalAlignment = dataAlign;
            cell.Colspan = 4;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            */
            #endregion
            #endregion FILA 8
            #endregion

            #region LINEA
            cell = new PdfPCell(new Phrase(linebreak));
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion

            #region DATOS DE EXAMENES
            #region FILA 9
            //Columna 1 ---------------------------------------------------------------------------------43
            string txtLabo = "PISO - [+] LABORAT.";
            cell = new PdfPCell(new Phrase(txtLabo, _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 44;
            cell.Border = 0;
            cell.PaddingBottom = 40;
            pdfWorkSpace.AddCell(cell);
            //Columna 2 ---------------------------------------------------------------------------------7
            string txtCheck = "Check";
            cell = new PdfPCell(new Phrase(txtCheck, _subTitleFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 6;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 9

            #region FILA 10
            //int i = 0;
            foreach (var row in data.Body)
            {
                PdfPTable tbLabo = new PdfPTable(50);
                cell = new PdfPCell(new Phrase(row.Cantidad, _subTitleFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 5;
                cell.BorderWidthTop = 5;
                cell.BorderWidthRight = 5;
                cell.BorderWidthBottom = 5;
                cell.BorderWidthLeft = 5;
                cell.PaddingTop = 25;
                cell.PaddingBottom = 35;
                tbLabo.AddCell(cell);

                cell = new PdfPCell(new Phrase(row.Description, _subTitleFont));
                cell.HorizontalAlignment = txtAlign;
                cell.Colspan = 40;
                cell.Border = 0;
                cell.PaddingTop = 30;
                cell.PaddingLeft = 30;
                tbLabo.AddCell(cell);

                string check = row.Check == "true" ? "X" : " ";
                cell = new PdfPCell(new Phrase(check, _subTitleFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 5;
                cell.BorderWidthTop = 5;
                cell.BorderWidthRight = 5;
                cell.BorderWidthBottom = 5;
                cell.BorderWidthLeft = 5;
                cell.PaddingTop = 25;
                cell.PaddingBottom = 35;
                tbLabo.AddCell(cell);
                //-----------------------------------------------------------------------------------------------------------
                cell = new PdfPCell(tbLabo);
                cell.Colspan = 50;
                cell.BorderWidth = 0;
                cell.PaddingBottom = 15;
                pdfWorkSpace.AddCell(cell);

                //i++;
            }
            #endregion FILA 10
            #endregion

            #region OBSERVACION
            #region FILA 11
            //Columna 1 
            string txtObs = "[+] OBSERVACION " + twoPoints;
            cell = new PdfPCell(new Phrase(txtObs, _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingTop = 50;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 11

            #region FILA 12
            cell = new PdfPCell(new Phrase(data.Head.Observacion, _subTitleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 12
            #endregion

            #region CREDENCIALES
            #region FILA 13
            //Columna 1
            string txtWebUser = "*** Usuario Web";
            cell = new PdfPCell(new Phrase(txtWebUser, _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 18;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(arrow, _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 5;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 3
            cell = new PdfPCell(new Phrase(data.Head.UsuarioWeb + " ***", _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 27;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 13

            #region FILA 14
            //Columna 1
            string txtPass = "*** Contraseña Web";
            cell = new PdfPCell(new Phrase(txtPass, _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 18;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            //Columna 2
            cell = new PdfPCell(new Phrase(arrow, _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 5;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            //Columna 3
            cell = new PdfPCell(new Phrase(data.Head.Contrasena + " ***", _titleFont));
            cell.HorizontalAlignment = txtAlign;
            cell.Colspan = 27;
            cell.Border = 0;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 14
            #endregion

            #region LINEA
            cell = new PdfPCell(new Phrase(linebreak));
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion

            #region FILA 15 FOOTER
            string txtFooter = "SUIZA LAB - GARANTIA DE UN DIAGNOSTICO SEGURO";
            cell = new PdfPCell(new Phrase(txtFooter, _titleFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion FILA 15

            #region FINAL
            string txtEnd = "----------------------------------------------------------------------------------------";
            cell = new PdfPCell(new Phrase(txtEnd, _titleFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 50;
            cell.Border = 0;
            cell.PaddingBottom = 65;
            pdfWorkSpace.AddCell(cell);
            #endregion FINAL

            #endregion

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
