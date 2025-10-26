using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.Interfaces
{
    public interface IChatRepository : IGenericRepository<ChatSession, Guid>
    {
        Task<int> CountPendingAsync();
        Task<List<ChatSession>> GetChatSessionsByStatusAsync(ChatStatusEnum chatStatusEnum);
    }
}
