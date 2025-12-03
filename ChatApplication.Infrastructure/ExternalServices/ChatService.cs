using ChatApplication.Application.DTOs.Chat.Response;
using ChatApplication.Application.ExternalServiceInterfaces;
using ChatApplication.Application.Mappings;
using ChatApplication.Domain.CustomExceptions;
using ChatApplication.Domain.Entities;
using ChatApplication.Infrastructure.Hubs;
using ChatApplication.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Infrastructure.ExternalServices
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatService(ApplicationDbContext dbContext, IHubContext<ChatHub> chatHub)
        {
            _dbContext = dbContext;
            _chatHub = chatHub;
        }

        public async Task<Guid> GetOrCreatePrivateChatAsync(Guid userA, Guid userB, CancellationToken ct = default)
        {
            Guid chatId = await _dbContext.ChatUsers
                .Where(cu => cu.UserId == userA || cu.UserId == userB)
                .GroupBy(cu => cu.ChatId)
                .Where(gr => gr.Count() == 2)
                .Select(gr => gr.Key)
                .FirstOrDefaultAsync(ct);

            if (chatId != Guid.Empty)
            {
                return chatId;
            }

            Chat chat = new() { 
                 IsGroup = false
            };
            await _dbContext.Chats.AddAsync(chat, ct);

            _dbContext.ChatUsers.AddRange(
                new ChatUser { ChatId = chat.ChatId, UserId = userA },
                new ChatUser { ChatId = chat.ChatId, UserId = userB }
            );
            await _dbContext.SaveChangesAsync(ct);

            return chat.ChatId;
        }

        public async Task<MessageResponse> SendMessageAsync(Guid chatId, Guid senderId, string text, CancellationToken ct = default)
        {
            bool isMember = await _dbContext.ChatUsers
                .AnyAsync(cu => cu.ChatId == chatId && cu.UserId == senderId, ct);

            if (!isMember)
            {
                throw new ForbiddenException("Message sending is forbidden!");
            }

            Message message = new()
            {
                ChatId = chatId,
                SenderUserId = senderId,
                Text = text,
                SentAt = DateTime.UtcNow
            };

            await _dbContext.Messages.AddAsync(message, ct);
            await _dbContext.SaveChangesAsync(ct);

            MessageResponse response = message.ToMessageResponse();
            await _chatHub.Clients.Group(chatId.ToString())
                .SendAsync("ReceiveMessage", response, ct);
            return response;
        }

        public async Task<List<MessageResponse>> GetMessagesAsync(Guid chatId, Guid userId, CancellationToken ct = default)
        {
            bool isMember = await _dbContext.ChatUsers
                .AnyAsync(cu => cu.ChatId == chatId && cu.UserId == userId, ct);

            if (!isMember)
            {
                throw new ForbiddenException("Message fetching is forbidden!");
            }

            return await _dbContext.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .Select(m => m.ToMessageResponse())
                .ToListAsync(ct);
        }
    }
}
