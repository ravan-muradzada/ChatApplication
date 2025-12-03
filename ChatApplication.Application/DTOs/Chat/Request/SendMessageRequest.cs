using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Application.DTOs.Chat.Request
{
    public sealed record SendMessageRequest(string Text);
}
