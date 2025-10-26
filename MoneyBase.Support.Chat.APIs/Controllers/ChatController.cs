using Azure;
using Microsoft.AspNetCore.Mvc;
using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Application.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyBase.Support.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        #region Fields
        private readonly IChatProducer _producer;
        private readonly IChatAssignmentService _chatAssignmentService;

        public ChatController(IChatProducer producer, IChatAssignmentService chatAssignmentService)
        {
            _producer = producer;
            _chatAssignmentService = chatAssignmentService;
        }
        #endregion

        #region APIs

        /// <summary>
        /// Creates a chat, publishes it to RabbitMQ (fast)
        /// A background job then consumes the message and assigns an agent (in the Chat assignment microservice)
        /// For demo purposes, the userId information is received in the request.
        /// In a real implementation using authentication (JWT, OAuth, etc.), user details would be extracted from the authentication token.
        /// </summary>
        /// <param name="req">ChatRequestDto.</param>
        /// <returns>Returns chatId</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat([FromBody] ChatRequestDto req)
        {
            await _producer.PublishAsync(req);
            return Ok(new { id = req.ChatId });
        }

        /// <summary>
        /// Poll endpoint for chat status.
        /// Currently, the frontend polls this endpoint every 1 second to check chat updates.
        /// This API keeps the demo simple and testable. in Agent Handler Micro service we push the chat updates 
        /// after handled by agant to the front end using SignalR hub also we can make this pull as realtime using the SignalR
        /// </summary>
        [HttpPost("poll/{id}")]
        public async Task<IActionResult> Poll([Required] [FromRoute] Guid id)
        {
            var response = await _chatAssignmentService.UpdateChatAsync(id);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        /// <summary>
        /// Check chat status, read from DB for demo purpose
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("status/{id}")]
        public async Task<IActionResult> CheckStatus([Required][FromRoute] Guid id)
        {
            var response = await _chatAssignmentService.CheckChatStatussAsync(id);
            if (!response.Success) 
                return BadRequest(response);
            return Ok(response);
        }

        #endregion
    }
}
