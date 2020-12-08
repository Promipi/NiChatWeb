using System;
using System.Collections.Generic;

#nullable disable

namespace NiChatWeb.SERVER.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Gmail { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
