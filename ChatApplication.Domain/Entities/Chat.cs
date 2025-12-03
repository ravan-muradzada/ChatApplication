using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Domain.Entities
{
    public class Chat
    {
        public Guid ChatId { get; set; } = Guid.NewGuid();
        public string? ChatName { get; set; }
        public bool IsGroup { get; set; } = false;
        public List<Message> Messages { get; set; } = new();
        public List<ChatUser> ChatUsers { get; set; } = new();
    }
}
