using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NotesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public ActionResult<User> GetUser()
        {
            return new User
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = User.Identity.Name,
            };
        }
    }
}
