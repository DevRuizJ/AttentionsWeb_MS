using Application.Commom.Status;
using Application.Pattern.Attention.Create.New;
using Application.Pattern.Attention.Create.Ticket;
using Application.Pattern.Attention.Read.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Attention
{
    public interface IAttentionRepository
    {
        Task<(ServiceStatus, string, NewAttentionResponse)> NewAttention(NewAttentionRequest request);
        Task<(ServiceStatus, string, AttentionListResponse)> AttentionList(AttentionListRequest request);
        Task<(ServiceStatus, string, AttentionTicketResponse)> AttentionTicket(AttentionTicketRequest request);
    }
}
