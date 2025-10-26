using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.Interfaces
{
    public interface IAgentService
    {
        Task<APIResponse<IEnumerable<AgentDto>>> LoadAgentsAsync();
        Task<APIResponse<AgentDto>> GetByIdAsync(Guid agentId);
        Task<APIResponse<AgentDto>> AddAgentAsync(AgentDto agentDto);
        Task<APIResponse<AgentDto>> UpdateAgentAsync(Guid agentId, AgentDto agentDto);
    }
}
