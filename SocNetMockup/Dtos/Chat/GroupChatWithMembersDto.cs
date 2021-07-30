using System;
using System.Collections.Generic;

namespace SocNetMockup.Dtos.Chat
{
    public class GroupChatWithMembersDto : GroupChatDto
    {
        public IEnumerable<GroupChatMemberDto> Members { get; set; }
    }
}
