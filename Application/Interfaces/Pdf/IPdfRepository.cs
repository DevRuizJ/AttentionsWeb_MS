using Application.Commom.Status;
using Application.Pattern.Attention.Create.Ticket.Data;
using Application.Pattern.Laboratory.Sample.Read.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Pdf
{
    public interface IPdfRepository
    {
        Task<(ServiceStatus, string, byte[])> AttentionTicket(AttentionTicketDataResponse data);
        Task<(ServiceStatus, string, byte[])> SampleBarcodeList(SampleListResponse data);
    }
}
