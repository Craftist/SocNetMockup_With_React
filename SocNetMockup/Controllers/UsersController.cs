using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocNetMockup.Data;
using SocNetMockup.DtoMappers;
using SocNetMockup.Dtos;
using SocNetMockup.Dtos.Chat;

namespace SocNetMockup.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly AppDbContext dbContext;

        public UsersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            return Ok(Mappers.User.Map(dbContext.Users));
        }

        [HttpGet("{userId:guid}")]
        public ActionResult<UserDto> GetUserById(Guid userId)
        {
            return Ok(Mappers.User.Map(dbContext.Users.Find(userId)));
        }

        [HttpGet("{userId:guid}/chats")]
        public ActionResult<IEnumerable<GroupChatDto>> GetUserById_GetChats(Guid userId)
        {
            var result = dbContext
                         .Users
                         .Include(x => x.GroupChatMemberships)
                         .ThenInclude(x => x.Chat)
                         .First(u => u.Id == userId)
                         .GroupChatMemberships
                         .Select(x => x.Chat);
            
            return Ok(ChatMappers.Chat.Map(result));
        }
    }
}
