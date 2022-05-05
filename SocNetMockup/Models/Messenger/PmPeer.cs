using System;
using System.Collections.Generic;

namespace SocNetMockup.Models.Messenger
{
    /// <summary>
    /// Private message peer.
    /// Holds from 1 to infinitely many users who are equal in permissions - therefore peers.
    /// Peers cannot kick each other or delete each others' messages, nor can they forbid others to pin messages, etc.
    /// </summary>
    public class PmPeer
    {
        public Guid Id { get; set; }
        public List<User> Members { get; set; }

        // Yes, you can even name your friends' DMs and put cover pictures on them!
        public Image Cover { get; set; }
        public string Title { get; set; }
        
        public IEnumerable<Message> Messages { get; set; }
    }
}

// TODO CRUD functionality for GroupChats (e.g. PUT /api/chats/asdd-rw2r-r23r23r23r-3r23r23rr/title with {"title": "New Title Value"} to change the title)
// TODO add GroupChatRole with many bools for each permission, etc
// TODO ban/kick/give role/revoke role for GroupChat(Member)s 
//            TODO (e.g. POST /api/chats/r32f-23r2-23fr23r-23df23r23rf2/members/f23f-32f25f-23e2d23-43d23r(user ID not member ID they are not exposed) with {"action": "KICK"}  (EXAMPLE) to kick)
