using CleanArchitecture.Application.Common.DTO;

namespace CleanArchitecture.Application.Features.User.Commands;

public record SoftDeleteUserCommand : IRequest<ResultDto<bool>>
{
    public Guid Id { get; set; }

    public SoftDeleteUserCommand(Guid id)
    {
        Id = id;
    }

    public SoftDeleteUserCommand()
    {
        
    }
}