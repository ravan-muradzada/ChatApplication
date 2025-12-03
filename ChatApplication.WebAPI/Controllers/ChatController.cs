using ChatApplication.Application.DTOs.Chat.Request;
using ChatApplication.Application.DTOs.Chat.Response;
using ChatApplication.Application.ExternalServiceInterfaces;
using ChatApplication.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("private/{userAId:guid}")]
        public async Task<IActionResult> CreateOrGetPrivateChat([FromRoute] Guid userAId, CancellationToken ct = default) {
            Guid userBId = User.GetUserId();
            Guid chatId = await _chatService.GetOrCreatePrivateChatAsync(userAId, userBId, ct);
            return Ok(chatId);
        }

        [HttpPost("{chatId:guid}/message")]
        public async Task<IActionResult> Send([FromRoute] Guid chatId, [FromBody] SendMessageRequest request, CancellationToken ct = default)
        {
            Guid senderId = User.GetUserId();
            MessageResponse messageResponse = await _chatService.SendMessageAsync(chatId, senderId, request.Text, ct);
            return Ok(messageResponse);
        }

        [HttpGet("{chatId:guid}/messages")]
        public async Task<IActionResult> GetMessages([FromRoute] Guid chatId, CancellationToken ct = default)
        {
            Guid userId = User.GetUserId();
            List<MessageResponse> response = await _chatService.GetMessagesAsync(chatId, userId, ct);
            return Ok(response);
        }
    }
}
