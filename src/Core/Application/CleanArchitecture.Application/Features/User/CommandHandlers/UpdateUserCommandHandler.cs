using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Helper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.DTO.User;
using CleanArchitecture.Application.Features.User.Commands;
using CleanArchitecture.Domain.Entities.Roles;
using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.Application.Features.User.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultDto<UserDto>>
{
    private readonly IRepository<Domain.Entities.Users.User, Guid> _userRepository;
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IRepository<UserRole, Guid> _userRoleRepository;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(IRepository<Domain.Entities.Users.User, Guid> userRepository,
                                    IRepository<Role, int> roleRepository,
                                    IRepository<UserRole, Guid> userRoleRepository,
                                    ILogger<UpdateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _logger = logger;
    }

    public async Task<ResultDto<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<UserDto>();

        try
        {
            if (await _userRepository.GetByIdAsync(cancellationToken, request.Id) is { } user)
            {
                if (!string.IsNullOrWhiteSpace(request.FirstName))
                    user.FirstName = request.FirstName;

                if (!string.IsNullOrWhiteSpace(request.LastName))
                    user.LastName = request.LastName;

                if (!string.IsNullOrWhiteSpace(request.Email))
                    user.Email = request.Email;

                if (request.IsActive is not null)
                    user.IsActive = (bool)request.IsActive;

                if (!string.IsNullOrWhiteSpace(request.Password)
                 && !string.IsNullOrWhiteSpace(request.ConfirmPassword)
                 && request.Password == request.ConfirmPassword)
                {
                    user.PrivateKey = Guid.NewGuid().ToString().Replace("-", "") + DateTimeOffset.UtcNow.Ticks;
                    user.PublicKey = PasswordHelper.EncodePassword(user.PrivateKey);
                    user.Password = PasswordHelper.HashPassword(request.Password, user.PublicKey, user.PrivateKey);
                }

                if (request.RoleIds?.Any() ?? false)
                {
                    await _userRoleRepository.DeleteAsync(cancellationToken, ur => ur.UserId == user.Id);

                    if (await AddUserRoles(user.Id, request.RoleIds, cancellationToken))
                    {
                        if (await _userRepository.UpdateAsync(cancellationToken, user))
                        {
                            // Do Something
                        }
                    }
                }

                if (await _userRepository.UpdateAsync(cancellationToken, user))
                {
                    result.IsSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "Data updated successfully.";
                    result.Data = user.Adapt<UserDto>();
                }
                else
                {
                    result.IsSuccess = false;
                    result.StatusCode = 400;
                    result.Message = "Can't updated your requested data.";
                    result.AddError("1400", "Can't updated your requested User.");
                    result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
                }
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = 400;
                result.Message = "Can't find your requested User.";
                result.AddError("1400", "Can't find your requested User. Please check your information.");
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

    private async Task<bool> AddUserRoles(Guid userId, List<int> roleIds, CancellationToken cancellationToken)
    {
        var userRoles = await _roleRepository.Table.AsNoTracking()
                                             .Where(r => roleIds.Contains(r.Id))
                                             .Select(r => new UserRole
                                             {
                                                 RoleId = r.Id,
                                                 UserId = userId
                                             })
                                             .ToListAsync(cancellationToken);

        return userRoles.Any() && (await _userRoleRepository.AddRangeAsync(userRoles, cancellationToken)).Any();
    }
}
