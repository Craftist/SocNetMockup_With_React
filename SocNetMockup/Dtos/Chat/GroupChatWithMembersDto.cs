using System;
using System.Collections.Generic;

namespace SocNetMockup.Dtos.Chat
{
    public class GroupChatWithMembersDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public GroupChatMemberDto Owner { get; set; }

        public IEnumerable<GroupChatMemberDto> Members { get; set; }
    }
}
