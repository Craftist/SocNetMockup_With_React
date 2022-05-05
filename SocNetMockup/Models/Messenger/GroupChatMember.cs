using System;
using System.Collections.Generic;

namespace SocNetMockup.Models.Messenger
{
    public class GroupChatMember
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; }
        public IEnumerable<GroupChatRole> Roles { get; set; }

        public User User { get; set; }
        public GroupChat Chat { get; set; }

        public Guid? PeerId => User?.Id ?? Chat?.Id;
    }
}
