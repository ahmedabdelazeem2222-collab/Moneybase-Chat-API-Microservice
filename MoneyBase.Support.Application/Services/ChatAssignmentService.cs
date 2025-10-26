using Microsoft.Extensions.Logging;
using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Application.Interfaces;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Domain.Enums;
using MoneyBase.Support.Shared;
using System;

namespace MoneyBase.Support.Application.Services
{
    public class ChatAssignmentService : IChatAssignmentService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ChatAssignmentService> _logger;

        public ChatAssignmentService(IUnitOfWork unitOfWork, ILogger<ChatAssignmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Methods

        public async Task<APIResponse<string>> AssignChatAsync(Guid chatId, Guid agentId)
        {
            var chat = await _unitOfWork.Chats.GetByIdAsync(chatId);
            if (chat == null)
            {
                _logger.LogWarning("Chat {ChatId} not found", chatId);
                return APIResponse<string>.NotFound($"Chat {chatId} not found");
            }

            var agent = await _unitOfWork.Agents.GetByIdAsync(agentId);
            if (agent == null)
            {
                _logger.LogWarning("Agent {AgentId} not found", agentId);
                chat.ChatStatus = ChatStatusEnum.Refused;
                await _unitOfWork.Chats.UpdateAsync(chat);
                await _unitOfWork.CommitAsync();
                return APIResponse<string>.NotFound($"Agent {agentId} not found. Chat marked as refused.");
            }

            // Update chat info
            chat.AgentId = agent.Id;
            chat.AssignedAt = DateTime.UtcNow;
            chat.ChatStatus = ChatStatusEnum.Assigned;

            // Update agent info
            agent.AssignedChatIds.Add(chat.Id);

            await _unitOfWork.Chats.UpdateAsync(chat);
            await _unitOfWork.Agents.UpdateAsync(agent);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Chat {ChatId} assigned to Agent {AgentId}", chat.Id, agent.Id);

            return APIResponse<string>.Ok(chat.Id.ToString(), $"Chat {chat.Id} assigned to Agent {agent.Id}");
        }
        public async Task<APIResponse<ChatSessionDto>> UpdateChatAsync(Guid chatId)
        {
            try
            {
                var currentChat = await _unitOfWork.Chats.GetByIdAsync(chatId);
                if (currentChat == null) return null;

                currentChat.LastPollAtUtc = DateTime.UtcNow;
                await _unitOfWork.Chats.UpdateAsync(currentChat);
                await _unitOfWork.CommitAsync();

                ChatSessionDto chatSessionDto = MapChatSessionToChatSessionDto(currentChat);

                return APIResponse<ChatSessionDto>.Ok(chatSessionDto);
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ChatAssignmentService/UpdateChatAsync failed for chat {ChatId}", chatId);
                return APIResponse<ChatSessionDto>.InternalServerError("server error", null);
            }
        }
        public async Task<APIResponse<ChatSessionDto>> CheckChatStatussAsync(Guid chatId)
        {
            try
            {
                var currentChat = await _unitOfWork.Chats.GetByIdAsync(chatId);
                if (currentChat == null) return null;

                ChatSessionDto chatSessionDto = MapChatSessionToChatSessionDto(currentChat);
                return APIResponse<ChatSessionDto>.Ok(chatSessionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ChatAssignmentService/CheckChatStatussAsync failed for chat {ChatId}", chatId);
                return APIResponse<ChatSessionDto>.InternalServerError("server error", null);
            }
        }
        public async Task<APIResponse<int>> CountPendingAsync()
        {
            try
            {
                var chats = await _unitOfWork.Chats.GetAllAsync();
                var count = chats.Where(c => c.ChatStatus == ChatStatusEnum.Pending).Count();
                return APIResponse<int>.Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ChatAssignmentService/CountPendingAsync failed");
                return APIResponse<int>.InternalServerError("server error", null);
            }
        }
        private ChatSessionDto MapChatSessionToChatSessionDto(ChatSession chatSession)
        {
            ChatSessionDto chatSessionDto = new ChatSessionDto();
            chatSession.LastPollAtUtc = DateTime.UtcNow;
            chatSession.ChatStatus = chatSession.ChatStatus;
            chatSession.UserId = chatSession.UserId;
            chatSession.AgentId = chatSession.AgentId;
            return chatSessionDto;
        }
        #endregion
    }
}
