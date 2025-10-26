using Microsoft.AspNetCore.Mvc;
using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Application.Interfaces;

namespace MoneyBase.Support.Chat.APIs.Controllers
{

    /// <summary>
    /// this controller exposing APIs that are consumed by other microservices.
    /// Handles operations such as creating, updating, and assigning chats,
    /// and provides endpoints for agent management and queue information.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InternalController : ControllerBase
    {
        #region Fields
        private readonly IChatProducer _producer;
        private readonly IChatAssignmentService _chatAssignmentService;
        private readonly IAgentService _agentService;
        public InternalController(IChatProducer producer, IChatAssignmentService chatAssignmentService,
            IAgentService agentService)
        {
            _producer = producer;
            _chatAssignmentService = chatAssignmentService;
            _agentService = agentService;
        }
        #endregion

        [HttpPost("assign")]
        public async Task<IActionResult> AssignChat([FromBody] AssignChatRequest req)
        {
            var response = await _chatAssignmentService.AssignChatAsync(req.ChatId, req.AgentId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("get-all-agents")]
        public async Task<IActionResult> GetAllAgents()
        {
            var result = await _agentService.LoadAgentsAsync();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("get-agent/{id}")]
        public async Task<IActionResult> GetAgentById(Guid id)
        {
            var result = await _agentService.GetByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("add-agent")]
        public async Task<IActionResult> AddAgent([FromBody] AgentDto agentDto)
        {
            var result = await _agentService.AddAgentAsync(agentDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update-agent/{id}")]
        public async Task<IActionResult> UpdateAgent(Guid id, [FromBody] AgentDto agentDto)
        {
            var result = await _agentService.UpdateAgentAsync(id, agentDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
