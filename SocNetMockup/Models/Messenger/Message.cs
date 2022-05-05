using System;

namespace SocNetMockup.Models.Messenger
{
    /// <summary>
    /// Represents a message. Can be used in group chats and in private messages. 
    /// </summary>
    public class Message
    {
        public Guid Id { get; set; }
        public GroupChatMember Sender { get; set; }

        public GroupChat? Chat { get; set; }
        public PmPeer? Peer { get; set; }

        public string Body { get; set; }
    }

}
