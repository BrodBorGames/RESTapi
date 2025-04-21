using System.ComponentModel.DataAnnotations;

namespace WEBApiREST.Users
{
    public record LoginUserRequest
    (
        [Required] string Username,
        [Required] string Password);
}
