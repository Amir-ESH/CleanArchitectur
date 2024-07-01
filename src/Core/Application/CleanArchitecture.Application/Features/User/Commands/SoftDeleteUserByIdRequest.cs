using CleanArchitecture.Application.Common.DTO;

namespace CleanArchitecture.Application.Features.User.Commands;

public record SoftDeleteUserByIdRequest : IRequest<ResultDto<bool>>
{
    public Guid Id { get; set; }

    public SoftDeleteUserByIdRequest(Guid id)
    {
        Id = id;
    }

    public SoftDeleteUserByIdRequest()
    {
        
    }
}