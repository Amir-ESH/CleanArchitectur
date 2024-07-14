using CleanArchitecture.Application.Common.DTO;

namespace CleanArchitecture.Application.Features.User.Commands;

public class HardDeleteUserCommand : IRequest<ResultDto<bool>>
{
    public Guid Id { get; set; }

    public HardDeleteUserCommand(Guid id)
    {
        Id = id;
    }

    public HardDeleteUserCommand()
    {
        
    }
}