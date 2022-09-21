using Application.Commom.Helpers;
using Application.Commom.Status;
using Application.Interfaces.Pdf;
using Application.Pattern.Attention.Create.Ticket.Data;
using Application.Pattern.Laboratory.Sample.Read.List;
using Domain.Formats.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PdfRepository : IPdfRepository
    {
        public async Task<(ServiceStatus, string, byte[])> AttentionTicket(AttentionTicketDataResponse data)
        {
            data.Head.Barcode = Utilities.CreateBarCode(data.Head.Ticket);

            var response = GetAttentionTicket.GetPDFTicketByteArray(data);

            if (response == null)
            {
                return (ServiceStatus.InternalError, "Pdf no pudo ser generado", null);
            }

            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, byte[])> SampleBarcodeList(SampleListResponse data)
        {
            if (data.Samples == null)
            {
                return (ServiceStatus.InternalError, "No existen Muestras que generen Código de Barra", null);
            }

            foreach (var sample in data.Samples)
            {
                sample.BarCode = Utilities.CreateBarCode(sample.Codigo);
            }

            //var sampleCodeBar = await _generatePdf.GetByteArray($"Formats/SampleBarcode.cshtml", data);
            var sampleCodeBar = SampleBarCode.GetPDFSampleBarCodeByteArray(data);

            if (sampleCodeBar == null)
            {
                return (ServiceStatus.InternalError, "Pdf no pudo ser generado", null);
            }

            return (ServiceStatus.Ok, null, sampleCodeBar);
        }
    }
}
