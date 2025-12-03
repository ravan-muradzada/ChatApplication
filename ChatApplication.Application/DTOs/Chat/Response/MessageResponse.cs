using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.DTOs.Chat.Response
{
    public sealed record MessageResponse(Guid ChatId, Guid SenderId, string Text, DateTime SentAt);
}
