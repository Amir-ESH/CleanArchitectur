using CleanArchitecture.Application.Common.DTO;

namespace CleanArchitecture.Application.Features.User.Commands;

public class DeleteUserByIdRequest : IRequest<ResultDto<bool>>
{
    public Guid Id { get; set; }

    public DeleteUserByIdRequest(Guid id)
    {
        Id = id;
    }

    public DeleteUserByIdRequest()
    {
        
    }
}