using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace IdentityMicroservices
{
    public record UserDto(Guid Id, string Username, string Email, decimal Gil, DateTimeOffset CreateDate);

    public record UpdateUserDto(
        [System.ComponentModel.DataAnnotations.Required][EmailAddress] string Email,
        [Range(0, 1000000)]decimal Gil);
}
