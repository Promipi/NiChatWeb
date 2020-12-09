using System;
using System.Collections.Generic;

#nullable disable

namespace NiChatWeb.SERVER.Models
{
    public partial class UserChat
    {
        public int Id { get; set; }
        public int? FUser { get; set; }
        public int? FChat { get; set; }
        public DateTime? DateJoin { get; set; }
        public int? Counter { get; set; }

        public virtual Chat FchatNavigation { get; set; }
        public virtual User FuserNavigation { get; set; }
    }
}
