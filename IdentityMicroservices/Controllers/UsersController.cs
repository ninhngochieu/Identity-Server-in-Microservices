using IdentityMicroservices.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityMicroservices.Controllers
{
    [Route("users")]
    [ApiController]
    [Authorize(Policy = LocalApi.PolicyName)] 
    ///Cơ chế bảo vệ API của Identity Server
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var users = _userManager.Users.ToList().Select(u => u.AsDto());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> PutAsync(string id, UpdateUserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            else
            {
                user.Email = userDto.Email;
                user.UserName = userDto.Email;
                user.Gil = userDto.Gil;

                await _userManager.UpdateAsync(user);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            else
            {
                await _userManager.DeleteAsync(user);

                return NoContent();
            }
        }
    }
}
