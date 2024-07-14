using CleanArchitecture.Application.Common.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CleanArchitecture.Application.DTO.Role;

public class RoleDto : BaseDto<int>
{
    [DisplayName("Last Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string Name { get; set; } = null!;

    [DisplayName("Is Active?")]
    public bool IsActive { get; set; }

    public ICollection<Domain.Entities.Users.User> Users { get; set; } = new List<Domain.Entities.Users.User>();
}