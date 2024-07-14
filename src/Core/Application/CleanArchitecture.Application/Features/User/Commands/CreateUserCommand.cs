using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CleanArchitecture.Application.Features.User.Commands;

public class CreateUserCommand
    : IRequest<ResultDto<UserDto>>
{
    [DisplayName("First Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string FirstName { get; set; } = null!;

    [DisplayName("Last Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string LastName { get; set; } = null!;

    [DisplayName("Email Address")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} Is not valid of data Type.")]
    public string Email { get; set; } = null!;

    //[DisplayName("Is Active?")]
    //public bool IsActive { get; set; }

    [DisplayName("Password")]
    [StringLength(128, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string Password { get; set; } = null!;

    [DisplayName("Confirm Password")]
    [StringLength(128, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;

    [DisplayName("User Roles")]
    public List<int> RoleIds { get; set; } = null!;

    public CreateUserCommand(string firstName, string lastName, string email/*, bool isActive*/, string password,
                          string confirmPassword, List<int> roleIds)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        //IsActive = isActive;
        Password = password;
        ConfirmPassword = confirmPassword;
        RoleIds = roleIds;
    }

    public CreateUserCommand()
    {
        
    }
}
