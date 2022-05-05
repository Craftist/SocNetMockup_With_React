using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocNetMockup.Data;
using SocNetMockup.Dtos.Chat;
using SocNetMockup.Models.Messenger;

namespace SocNetMockup.SignalR
{
    [Authorize]
    class ChatHub : Hub
    {
        private readonly AppDbContext dbContext;

        public ChatHub(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected: ConnectionId='{Context.ConnectionId}', UserId='{Context.UserIdentifier}'");
        }

        public async Task Send(MessageDto message)
        {
            if (message.Chat != null) {
                await Clients.Group($"In-Chat-{message.Chat.Id}").SendAsync("OnMessage", message);
                Console.WriteLine($"Sent '{message.Body}' to chat '{message.Chat.Id}'");
            } else if (message.Peer != null) {
                await Clients.Group($"In-Peer-{message.Peer.Id}").SendAsync("OnMessage", message);
                Console.WriteLine($"Sent '{message.Body}' to peer '{message.Peer.Id}'");
            }
        }

        public async Task<object> OnMessageSimple(Guid chatOrPmPeerId, string message)
        {
            var sender = dbContext.GroupChatMembers.FirstOrDefault(gcm => gcm.User.Id == Guid.Parse(Context.UserIdentifier));

            Message messageObject = null;

            var maybeGroupChat = dbContext.GroupChats.FirstOrDefault(gc => gc.Id == chatOrPmPeerId);
            if (maybeGroupChat is not null) {
                messageObject = new Message {
                    Body = message,
                    Chat = maybeGroupChat,
                    Sender = sender
                };
            } else {
                var maybePmPeer = dbContext.PmPeers.FirstOrDefault(pmp => pmp.Id == chatOrPmPeerId);
                if (maybePmPeer is not null) {
                    messageObject = new Message {
                        Body = message,
                        Peer = maybePmPeer,
                        Sender = sender
                    };
                }
            }

            if (messageObject is not null) {
                dbContext.GroupChatMessages.Add(messageObject);
                await dbContext.SaveChangesAsync();
            } else {
                return new { success = false, reason = "The ID supplied is neither a chat nor a PmPeer id." };
            }
            
            
            
            Console.WriteLine($"Received simple message in C#: {message}\n  From: '{Context.UserIdentifier}', connection '{Context.ConnectionId}'");
            return new { success = true };
        }
    }
}
