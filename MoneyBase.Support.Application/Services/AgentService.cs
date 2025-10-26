using Microsoft.Extensions.Logging;
using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Application.Interfaces;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.Services
{
    public class AgentService : IAgentService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AgentService> _logger;
        #endregion

        #region Methods
        public AgentService(IUnitOfWork unitOfWork, ILogger<AgentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<APIResponse<IEnumerable<AgentDto>>> LoadAgentsAsync()
        {
            try
            {

                var agents = await _unitOfWork.Agents.GetAllAsync();
                var dtos = agents.Select(a => MapToDto(a));
                return APIResponse<IEnumerable<AgentDto>>.Ok(dtos);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "AgentService/LoadAgentsAsync Error");
                return APIResponse<IEnumerable<AgentDto>>.InternalServerError("server error");
            }

        }
        public async Task<APIResponse<AgentDto>> GetByIdAsync(Guid agentId)
        {
            try
            {
                var agent = await _unitOfWork.Agents.GetByIdAsync(agentId);
                if (agent == null)
                    return APIResponse<AgentDto>.NotFound("Agent not found");

                return APIResponse<AgentDto>.Ok(MapToDto(agent));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgentService/LoadAgentsAsync Error");
                return APIResponse<AgentDto>.InternalServerError("server error");
            }
        }
        public async Task<APIResponse<AgentDto>> AddAgentAsync(AgentDto agentDto)
        {
            try
            {
                var agent = MapToEntity(agentDto);
                await _unitOfWork.Agents.AddAsync(agent);
                await _unitOfWork.CommitAsync();
                return APIResponse<AgentDto>.Ok(MapToDto(agent), "Agent added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgentService/LoadAgentsAsync Error");
                return APIResponse<AgentDto>.InternalServerError("server error");
            }
        }
        public async Task<APIResponse<AgentDto>> UpdateAgentAsync(Guid agentId, AgentDto agentDto)
        {
            try
            {
                var agent = await _unitOfWork.Agents.GetByIdAsync(agentId);
                if (agent == null) return APIResponse<AgentDto>.NotFound("Agent not found");

                // Update properties
                agent.Name = agentDto.Name;
                agent.Seniority = agentDto.Seniority;
                agent.ShiftStartHour = agentDto.ShiftStartHour;
                agent.ShiftEndHour = agentDto.ShiftEndHour;
                agent.IsOverflow = agentDto.IsOverflow;
                agent.AssignedChatIds.Clear();
                foreach (var chatId in agentDto.AssignedChatIds)
                    agent.AssignedChatIds.Add(chatId);

                await _unitOfWork.Agents.UpdateAsync(agent);
                await _unitOfWork.CommitAsync();
                return APIResponse<AgentDto>.Ok(MapToDto(agent), "Agent updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgentService/LoadAgentsAsync Error");
                return APIResponse<AgentDto>.InternalServerError("server error");
            }
        }

        /// <summary>
        /// I preferred Manual mapping, as automated mapping tools like auto mapper
        // can cause issues or incorrect results somtimes and need many config to handling complex objects.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        private static AgentDto MapToDto(Agent agent) => new()
        {
            Name = agent.Name,
            Seniority = agent.Seniority,
            ShiftStartHour = agent.ShiftStartHour,
            ShiftEndHour = agent.ShiftEndHour,
            IsOverflow = agent.IsOverflow,
            AssignedChatIds = agent.AssignedChatIds
        };
        private static Agent MapToEntity(AgentDto dto) => new()
        {
            Name = dto.Name,
            Seniority = dto.Seniority,
            ShiftStartHour = dto.ShiftStartHour,
            ShiftEndHour = dto.ShiftEndHour,
            IsOverflow = dto.IsOverflow,
            AssignedChatIds = dto.AssignedChatIds != null ? new HashSet<Guid>(dto.AssignedChatIds) : new HashSet<Guid>()
        };
        #endregion
    }
}
