using System;
using SocNetMockup.Models.Messenger;

namespace SocNetMockup.Dtos.Chat
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public GroupChatMemberDto Sender { get; set; }

        public GroupChatDto? Chat { get; set; }
        public PmPeer? Peer { get; set; }

        public string Body { get; set; }
    }
}
