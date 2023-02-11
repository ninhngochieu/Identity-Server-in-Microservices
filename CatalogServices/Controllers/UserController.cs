using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public const string AdminRole = "Admin";
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = User;
            //User phải có Role mới sử dụng được hàm này
            var currentUserId = User.FindFirstValue("sub");
            
            if (Guid.Parse(currentUserId) != Guid.Empty) 
            {
                if (!User.IsInRole(AdminRole))
                {
                    return Unauthorized("Custom authentication is working");
                }
            }

            return Ok();
        }
    }
}
