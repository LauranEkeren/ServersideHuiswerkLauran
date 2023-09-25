using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [UIHint("password")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        public string ReturnUrl { get; set; } = "/";

    }
}
