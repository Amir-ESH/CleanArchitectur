using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.DTO.Controller.AuthenticationController;

public class AuthenticationRequestBodyDto
{
    [Display(Name = "Email Address")]
    [Required]
    [MaxLength(50)]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; } = default!;

    [Display(Name = "Password")]
    [Required]
    [MaxLength(50)]
    public required string Password { get; set; } = default!;

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; } = false;
}
