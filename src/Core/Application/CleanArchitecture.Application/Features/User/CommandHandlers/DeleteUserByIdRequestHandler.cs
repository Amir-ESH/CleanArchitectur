using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Features.User.Commands;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.User.CommandHandlers;

public class DeleteUserByIdRequestHandler : IRequestHandler<DeleteUserByIdRequest, ResultDto<bool>>
{
    private readonly IRepository<Domain.Entities.User, Guid> _userRepository;
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IRepository<UserRole, Guid> _userRoleRepository;
    private readonly ILogger<DeleteUserByIdRequestHandler> _logger;

    public DeleteUserByIdRequestHandler(IRepository<Domain.Entities.User, Guid> userRepository,
                                        IRepository<Role, int> roleRepository,
                                        IRepository<UserRole, Guid> userRoleRepository,
                                        ILogger<DeleteUserByIdRequestHandler> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _logger = logger;
    }

    public async Task<ResultDto<bool>> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<bool>();

        try
        {
            if (await _userRepository.DeleteAsync(cancellationToken, new Domain.Entities.User {Id = request.Id}))
            {
                if (await _userRoleRepository.DeleteAsync(cancellationToken, ur => ur.UserId == request.Id))
                {
                    result.IsSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "Data deleted successfully.";
                    result.Data = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.StatusCode = 200;
                    result.Message = "Data deleted correctly successfully. User Role not deleted correctly.";
                    result.Data = true;
                }
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = 400;
                result.Message = "Can't deleted your requested data.";
                result.AddError("1400", "Can't deleted your requested User.");
                result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");

            result.IsSuccess = false;
            result.StatusCode = 500;
            result.Message = "Something went wrong";
            result.ResponseType = "Exception error";
            result.AddError("1500", ex.Message);
            result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
        }

        return result;
    }
}