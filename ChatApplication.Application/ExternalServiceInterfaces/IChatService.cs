using ChatApplication.Application.DTOs.Chat.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.ExternalServiceInterfaces
{
    public interface IChatService
    {
        Task<Guid> GetOrCreatePrivateChatAsync(Guid userA, Guid userB, CancellationToken ct = default);
        Task<MessageResponse> SendMessageAsync(Guid chatId, Guid senderId, string text, CancellationToken ct = default);
        Task<List<MessageResponse>> GetMessagesAsync(Guid chatId, Guid userId, CancellationToken ct = default);
    }
}
