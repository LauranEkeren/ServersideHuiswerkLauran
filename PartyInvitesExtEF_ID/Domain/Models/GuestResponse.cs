using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;
public class GuestResponse
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter your name")]
    [Display(Name = "Your name:")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter your email address")]
    [Display(Name = "Your email:")]
    [EmailAddress]
    [Compare(nameof(EmailValidation))]
    public string? Email { get; set; }
    [Display(Name = "Herhaal email:")]
    [NotMapped]
    public string? EmailValidation { get; set; }

    [Required(ErrorMessage = "Please enter your phone number")]
    [Display(Name = "Your phone nr:")]
    [Phone]
    public string? Phone { get; set; }


    [Required(ErrorMessage = "Please specify whether you'll attent")]
    [Display(Name = "Will you attend?")]
    public bool? WillAttent { get; set; }
}
