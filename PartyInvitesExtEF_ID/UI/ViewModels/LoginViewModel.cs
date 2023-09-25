using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels;
public class LoginViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [UIHint("Password")]
    [PasswordPropertyText]
    public string Password { get; set; } = null!;
    public string ReturnUrl { get; set; } = "/";
}
