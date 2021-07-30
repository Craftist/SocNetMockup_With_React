using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SocNetMockup.Models.Chat;

namespace SocNetMockup.Models
{
    public class User : IdentityUser<Guid>
    {
        public IEnumerable<GroupChatMember> GroupChatMemberships { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
