using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Features.User.Commands;

namespace CleanArchitecture.Application.Features.User.CommandHandlers;

public class SoftDeleteUserByIdRequestHandler : IRequestHandler<SoftDeleteUserByIdRequest, ResultDto<bool>>
{
    private readonly IRepository<Domain.Entities.User, Guid> _userRepository;
    private readonly ILogger<SoftDeleteUserByIdRequestHandler> _logger;

    public SoftDeleteUserByIdRequestHandler(IRepository<Domain.Entities.User, Guid> userRepository, ILogger<SoftDeleteUserByIdRequestHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ResultDto<bool>> Handle(SoftDeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<bool>();

        try
        {
            if (await _userRepository.GetByIdAsync(cancellationToken,  request.Id) is { } user)
            {
                user.IsDeleted = true;

                if (await _userRepository.UpdateAsync(cancellationToken, user))
                {
                    result.IsSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "Data deleted successfully.";
                    result.Data = true;
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