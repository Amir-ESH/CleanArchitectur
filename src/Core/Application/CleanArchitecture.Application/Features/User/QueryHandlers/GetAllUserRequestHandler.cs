using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.DTO.User;
using CleanArchitecture.Application.Features.User.Queries;

namespace CleanArchitecture.Application.Features.User.QueryHandlers;

public class GetAllUserRequestHandler : IRequestHandler<GetAllUserRequest, ResultDto<List<UserDto>>>
{
    private readonly IRepository<Domain.Entities.User, Guid> _userRepository;
    private readonly ILogger<GetAllUserRequestHandler> _logger;

    public GetAllUserRequestHandler(IRepository<Domain.Entities.User, Guid> userRepository,
                                    ILogger<GetAllUserRequestHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ResultDto<List<UserDto>>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<List<UserDto>>();

        try
        {
            if (await _userRepository.GetAllAsync(cancellationToken) is { } users)
            {
                result.IsSuccess = true;
                result.StatusCode = 200;
                result.Message = "Data received successfully.";
                result.Data = users.Adapt<List<UserDto>>();
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = 404;
                result.Message = "Can't find your requested data.";
                result.AddError("1404", "Can't find your requested User Information.");
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
