using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.User;

namespace CleanArchitecture.Application.Features.User.Queries;

public class GetUserByIdRequest : IRequest<ResultDto<UserDto>>
{
    public Guid Id { get; set; }

    public GetUserByIdRequest(Guid id)
    {
        Id = id;
    }

    public GetUserByIdRequest()
    {
        
    }
}
