using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.User;

namespace CleanArchitecture.Application.Features.User.Queries;

public class GetUserByIdQuery : IRequest<ResultDto<UserDto>>
{
    public Guid Id { get; set; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }

    public GetUserByIdQuery()
    {
        
    }
}
