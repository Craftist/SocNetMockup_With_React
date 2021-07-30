using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocNetMockup.Data;
using SocNetMockup.Data.ApiResponse;
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

            return this.ApiOk(ChatMappers.Chat.Map(chats));
        }

        [Authorize, HttpGet("{chatId:guid}")]
        public IActionResult GetById(Guid chatId) => Ok();

        public record CreateChatInputModel(string title, Guid[] initialMembers);

        [Authorize, HttpPost("")]
        public IActionResult CreateChat(CreateChatInputModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.title)) {
                return this.ApiErr(ApiError.RequiredStringParamEmpty(nameof(inputModel.title)));
            }

            var thisUser = dbContext.Users.Get(User);

            var chat = new GroupChat {
                Title = inputModel.title
            };

            if (inputModel.initialMembers == null) {
                inputModel = inputModel with { initialMembers = new Guid[] { } };
            }

            var initialMemberUsers = inputModel.initialMembers.Select(x => dbContext.Users.Find(x));
            var initialMembers = initialMemberUsers.Select(x => new GroupChatMember {
                User = x,
                Chat = chat
            });

            var ownerMember = new GroupChatMember { User = thisUser, Chat = chat };
            dbContext.GroupChatMembers.Add(ownerMember);

            chat.OwnerId = ownerMember.Id;
            chat.Members = initialMembers.ToList();

            dbContext.GroupChatMembers.AddRange(chat.Members);
            dbContext.GroupChats.Add(chat);

            Console.WriteLine("chat id = " + chat.Id);
            ownerMember.Chat = chat;
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = chat.Id }, ChatMappers.ChatWithMembers.Map(chat));
        }
    }
}
// Console.WriteLine($"ownerMember {{ Id = {ownerMember.Id}, UserId = {ownerMember.User.Id}, ChatId = {ownerMember.Chat.Id} }}");
// Console.WriteLine($"chat {{ Id = {chat.Id}, OwnerId = {chat.OwnerId}, Title = {chat.Title} }}");
