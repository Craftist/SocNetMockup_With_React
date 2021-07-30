using System;
using System.Collections.Generic;

namespace SocNetMockup.Models.Chat
{
    public class GroupChat
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// ID of a GroupChatMember of a chat owner.
        /// </summary>
        public Guid OwnerId { get; set; }
        public IEnumerable<GroupChatMember> Members { get; set; }
    }
}
