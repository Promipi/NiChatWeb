using System;
using System.Collections.Generic;

#nullable disable

namespace NiChatWeb.SERVER.Models
{
    public partial class UserChat
    {
        public int? FUser { get; set; }
        public int? Fchat { get; set; }
        public DateTime? DateJoin { get; set; }
        public int? Counter { get; set; }

        public virtual Chat FchatNavigation { get; set; }
        public virtual User FuserNavigation { get; set; }
    }
}
