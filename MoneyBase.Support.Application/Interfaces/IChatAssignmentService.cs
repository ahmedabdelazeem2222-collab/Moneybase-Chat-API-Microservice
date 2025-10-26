using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.Interfaces
{
    public interface IChatAssignmentService
    {
        Task<APIResponse<string>> AssignChatAsync(Guid chatId, Guid agentId);
        Task<APIResponse<ChatSessionDto>> UpdateChatAsync(Guid chatId);
        Task<APIResponse<ChatSessionDto>> CheckChatStatussAsync(Guid chatId);
        Task<APIResponse<int>> CountPendingAsync();
    }
}
