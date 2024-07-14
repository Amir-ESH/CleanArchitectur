using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.User;

namespace CleanArchitecture.Application.Features.User.Queries;

public class GetUserByEmailAddressQuery : IRequest<ResultDto<UserDto?>>
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public GetUserByEmailAddressQuery(string email)
    {
        Email = email;
    }

    public GetUserByEmailAddressQuery() : this(null!)
    {
        
    }
}