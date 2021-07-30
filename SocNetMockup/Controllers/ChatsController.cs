using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocNetMockup.Data;
using SocNetMockup.DtoMappers;
using SocNetMockup.Models.Chat;
using SocNetMockup.Util;

namespace SocNetMockup.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ChatsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize, HttpGet("")]
        public IActionResult GetMyChats()
        {
            var user = dbContext.Users
                                .Include(x => x.GroupChatMemberships)
                                .ThenInclude(x => x.Chat)
                                .Get(User);

            var chats = user.GroupChatMemberships.Select(x => x.Chat);

            return Ok(ChatMappers.Chat.Map(chats));
        }

        [Authorize, HttpGet("{chatId:guid}")]
        public IActionResult GetById(Guid chatId) => Ok();

        public record CreateChatInputModel(string title, Guid[] initialMembers);

        [Authorize, HttpPost("")]
        public IActionResult CreateChat(CreateChatInputModel inputModel)
        {
            var thisUser = dbContext.Users.Get(User);
            
            var chat = new GroupChat {
                Title = inputModel.title
            };
            
            var initialMemberUsers = inputModel.initialMembers.Select(x => dbContext.Users.Find(x));
            var initialMembers = initialMemberUsers.Select(x => new GroupChatMember {
                User = x,
                Chat = chat
            });
            
            var ownerMember = new GroupChatMember { User = thisUser, Chat = chat };

            dbContext.Add(ownerMember);
            dbContext.SaveChanges();

            chat.OwnerId = ownerMember.Id;
            chat.Members = new[] { ownerMember }.Concat(initialMembers);

            dbContext.AddRange(chat.Members);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = chat.Id }, ChatMappers.ChatWithMembers.Map(chat) );
        }
    }
}
