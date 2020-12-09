using System;
using System.Collections.Generic;

#nullable disable

namespace NiChatWeb.SERVER.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
            UserChats = new HashSet<UserChat>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Creation { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserChat> UserChats { get; set; }
    }
}
