using ChatApplication.Application.DTOs.Chat.Response;
using ChatApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.Mappings
{
    public static class MessageMappings
    {
        public static MessageResponse ToMessageResponse(this Message message)
        {
            return new MessageResponse(
                message.ChatId, message.SenderUserId, message.Text, message.SentAt
            );
        }
    }
}
