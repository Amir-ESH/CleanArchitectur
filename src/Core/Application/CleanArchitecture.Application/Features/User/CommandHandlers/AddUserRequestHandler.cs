using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Helper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.DTO.User;
using CleanArchitecture.Application.Features.User.Commands;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.User.CommandHandlers;

public class AddUserRequestHandler : IRequestHandler<AddUserRequest, ResultDto<UserDto>>
{
    private readonly IRepository<Domain.Entities.User, Guid> _userRepository;
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IRepository<UserRole, Guid> _userRoleRepository;

    private readonly ILogger<AddUserRequestHandler> _logger;

    public AddUserRequestHandler(IRepository<Domain.Entities.User, Guid> userRepository,
                                 IRepository<Role, int> roleRepository, IRepository<UserRole, Guid> userRoleRepository,
                                 ILogger<AddUserRequestHandler> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _logger = logger;
    }

    public async Task<ResultDto<UserDto>> Handle(AddUserRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<UserDto>();

        try
        {
            if (await _userRepository.GetAnyAsync(cancellationToken, u => u.Email == request.Email))
            {
                result.IsSuccess = false;
                result.StatusCode = 400;
                result.Message = "Can't added your requested data.";
                result.AddError("1400", 
                                "This Email All ready be used.", 
                                "Email");
                result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
            }
            else
            {
                if (await _userRepository.AddAsync(cancellationToken, InitAddUserRequest(request)) is { } user)
                {
                    if (await AddUserRoles(user.Id, request.RoleIds, cancellationToken))
                    {
                        result.IsSuccess = true;
                        result.StatusCode = 200;
                        result.Message = "Data received successfully.";
                        result.Data = user.Adapt<UserDto>();
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.StatusCode = 400;
                        result.Message = "Can't added your requested data.";
                        result.AddError("1400", "Can't added your requested User Role  Information.");
                        result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.StatusCode = 400;
                    result.Message = "Can't added your requested data.";
                    result.AddError("1400", "Can't added your requested User Information.");
                    result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
                }
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

    private Domain.Entities.User InitAddUserRequest(AddUserRequest request)
    {
        var addRequest = request.Adapt<Domain.Entities.User>();
        addRequest.PrivateKey = Guid.NewGuid().ToString().Replace("-", "") + DateTimeOffset.UtcNow.Ticks;
        addRequest.PublicKey = PasswordHelper.EncodePassword(addRequest.PrivateKey);
        addRequest.Password =
            PasswordHelper.HashPassword(request.Password, addRequest.PublicKey, addRequest.PrivateKey);

        return addRequest;
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
