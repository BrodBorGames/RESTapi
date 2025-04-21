using System.ComponentModel.DataAnnotations;

namespace WEBApiREST.Users
{
    public record RegisterUserRequest(
        [Required] string Username,
        [Required] string Password);
}
