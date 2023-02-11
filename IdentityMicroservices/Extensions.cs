using IdentityMicroservices.Areas.Identity.Data;

namespace IdentityMicroservices
{
    public static class Extensions
    {
        public static UserDto AsDto(this ApplicationUser user)
        {
            return new UserDto(Guid.Parse(user.Id), user.UserName, user.Email, user.Gil, user.CreateDate);
        }
    }
}
