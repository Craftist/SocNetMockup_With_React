using System;

namespace SocNetMockup.Dtos.Chat
{
    public class GroupChatDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public GroupChatMemberDto Owner { get; set; }
    }
}
