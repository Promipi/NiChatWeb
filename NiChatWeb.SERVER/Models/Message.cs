using System;
using System.Collections.Generic;

#nullable disable

namespace NiChatWeb.SERVER.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? FUser { get; set; }
        public int? Fchat { get; set; }
        public string Body { get; set; }
        public DateTime? Creation { get; set; }

        public virtual Chat FchatNavigation { get; set; }
        public virtual User FclientNavigation { get; set; }
    }
}
