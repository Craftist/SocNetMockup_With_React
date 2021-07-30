using System;

namespace SocNetMockup.Models.Chat
{
    public class GroupChatMember
    {
        public Guid Id { get; set; }

        public User User { get; set; }
        public GroupChat Chat { get; set; }
    }
}
