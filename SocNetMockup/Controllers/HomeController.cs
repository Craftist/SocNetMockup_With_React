using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocNetMockup.Controllers
{
    [ApiController, Route("Home")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Index");
        }
        
        [Authorize, HttpGet("getid")]
        public IActionResult GetId()
        {
            return Ok(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "null");
        }
    }
}
