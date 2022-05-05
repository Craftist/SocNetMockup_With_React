using System;

namespace SocNetMockup.Dtos.Chat
{
    public class GroupChatMemberDto
    {
        /// <summary>
        /// ID of a group chat member. Should never be exposed to the public API, since this value
        /// is meaningless to the end user. Always map it to the real User ID.
        /// </summary>
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
