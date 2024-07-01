using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CleanArchitecture.Application.Features.User.Commands;

public class UpdateUserRequest : IRequest<ResultDto<UserDto>>
{
    [DisplayName("User ID")]
    public Guid Id { get; set; }

    [DisplayName("First Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string? FirstName { get; set; }

    [DisplayName("Last Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string? LastName { get; set; }

    [DisplayName("Email Address")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} Is not valid of data Type.")]
    public string? Email { get; set; }

    [DisplayName("Password")]
    [StringLength(128, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string? Password { get; set; }

    [DisplayName("Confirm Password")]
    [StringLength(128, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }

    [DisplayName("Is Active?")]
    public bool? IsActive { get; set; }

    public List<int>? RoleIds { get; set; }

    public UpdateUserRequest(Guid id, string? firstName, string? lastName, string? email, string? password,
                             string? confirmPassword, bool? isActive, List<int>? roleIds)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
        IsActive = isActive;
        RoleIds = roleIds;
    }

    public UpdateUserRequest()
    {
        
    }
}
