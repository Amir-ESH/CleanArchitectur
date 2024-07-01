using CleanArchitecture.Application.Common.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using CleanArchitecture.Application.DTO.Role;

namespace CleanArchitecture.Application.DTO.User;

public class UserDto : BaseDto<Guid>
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

    [DisplayName("Password")]
    [StringLength(300, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string Password { get; set; } = null!;

    [DisplayName("Private Key")]
    [StringLength(64, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string PrivateKey { get; set; } = null!;

    [DisplayName("Public Key")]
    [StringLength(64, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string PublicKey { get; set; } = null!;

    [DisplayName("Is Active?")]
    public bool IsActive { get; set; }

    public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
}