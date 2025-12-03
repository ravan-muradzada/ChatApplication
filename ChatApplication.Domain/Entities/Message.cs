using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Domain.Entities
{
    public class Message
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public Guid SenderUserId { get; set; } 
        public string Text { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public Guid ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
    }
}
