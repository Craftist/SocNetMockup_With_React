using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocNetMockup.Data;
using SocNetMockup.Data.ApiResponse;
using SocNetMockup.Util;

namespace SocNetMockup.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public MessagesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet, Route("{chatOrPeerId:guid}")]
        public IActionResult GetById(Guid chatOrPeerId)
        {
            var maybeChat = dbContext.GroupChats.FirstOrDefault(x => x.Id == chatOrPeerId);
            if (maybeChat is not null) {
                return this.ApiOk(maybeChat);
            }

            var maybePeer = dbContext.PmPeers.FirstOrDefault(x => x.Id == chatOrPeerId);
            if (maybePeer is not null) {
                return this.ApiOk(maybePeer);
            }

            return this.ApiErr(ApiError.ChatDoesNotExist(chatOrPeerId));
        }
    }
}
