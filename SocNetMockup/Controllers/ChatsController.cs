using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocNetMockup.Data;
using SocNetMockup.Data.ApiResponse;
using SocNetMockup.DtoMappers;
using SocNetMockup.Models;
using SocNetMockup.Models.Messenger;
using SocNetMockup.Util;

namespace SocNetMockup.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public ChatsController(AppDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("")]
        public IActionResult GetMyChats()
        {
            var user = dbContext.Users
                                .Include(x => x.GroupChatMemberships)
                                .ThenInclude(x => x.Chat)
                                .Get(User);

            var chats = user.GroupChatMemberships.Select(x => x.Chat);

            return this.ApiOk(ChatMappers.Chat.Map(chats));
        }

        [HttpGet("{chatId:guid}")]
        public IActionResult GetById(Guid chatId)
        {
            var chat = dbContext.GroupChats.Include(x => x.Members).First(x => x.Id == chatId);

            if (chat == null) {
                return this.ApiErr(ApiError.ChatDoesNotExist(chatId));
            }

            return this.ApiOk(ChatMappers.Chat.Map(chat));
        }

        public record CreateChatInputModel(string title, Guid[] initialMembers);

        [HttpPost("")]
        public IActionResult CreateChat(CreateChatInputModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.title)) {
                return this.ApiErr(ApiError.RequiredStringParamEmpty(nameof(inputModel.title)));
            }

            var thisUser = dbContext.Users.Get(User);

            var chat = new GroupChat {
                Title = inputModel.title,
                CreationDate = DateTime.Now
            };

            if (inputModel.initialMembers == null) {
                inputModel = inputModel with { initialMembers = Array.Empty<Guid>() };
            }

            var initialMemberUsers = inputModel.initialMembers.Select(x => dbContext.Users.Find(x));
            var initialMembers = initialMemberUsers.Select(x => new GroupChatMember {
                User = x,
                Chat = chat
            });

            var ownerMember = new GroupChatMember { User = thisUser, Chat = chat };
            dbContext.GroupChatMembers.Add(ownerMember);
            dbContext.SaveChanges();

            chat.OwnerId = ownerMember.Id;
            chat.Members = initialMembers.ToList();

            dbContext.GroupChatMembers.AddRange(chat.Members);
            dbContext.SaveChanges();
            dbContext.GroupChats.Add(chat);
            dbContext.SaveChanges();

            //Console.WriteLine("chat id = " + chat.Id);
            ownerMember.Chat = chat;
            dbContext.SaveChanges();
            ownerMember.Chat = chat;
            dbContext.SaveChanges();

            return this.ApiOk(new {
                created = true,
                createdChat = ChatMappers.Chat.Map(chat),
                resourceAvailableAt = $"GET /api/chats/{chat.Id}"
            });
            //return CreatedAtAction(nameof(GetById), new { id = chat.Id }, ChatMappers.ChatWithMembers.Map(chat));
        }

        [HttpDelete("{chatId:guid}")]
        public IActionResult DeleteChatById(Guid chatId)
        {
            var chat = dbContext.GroupChats.Include(x => x.Members).First(x => x.Id == chatId);

            if (chat == null) {
                return this.ApiErr(ApiError.ChatDoesNotExist(chatId));
            }

            chat.Members = Enumerable.Empty<GroupChatMember>();

            dbContext.GroupChats.Remove(chat);
            dbContext.SaveChanges();

            return this.ApiOk(new {
                deletedChatId = chatId
            });
        }
    }
}
