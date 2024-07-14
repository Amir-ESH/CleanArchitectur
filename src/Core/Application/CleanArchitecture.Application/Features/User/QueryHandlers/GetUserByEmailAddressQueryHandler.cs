﻿using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.DTO.User;
using CleanArchitecture.Application.Features.User.Queries;
using Exception = System.Exception;

namespace CleanArchitecture.Application.Features.User.QueryHandlers;

public class GetUserByEmailAddressQueryHandler : IRequestHandler<GetUserByEmailAddressQuery, ResultDto<UserDto?>>
{
    private readonly IRepository<Domain.Entities.Users.User, Guid> _userRepository;
    private readonly ILogger<GetUserByEmailAddressQueryHandler> _logger;

    public GetUserByEmailAddressQueryHandler(IRepository<Domain.Entities.Users.User, Guid> userRepository,
                                               ILogger<GetUserByEmailAddressQueryHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ResultDto<UserDto?>> Handle(GetUserByEmailAddressQuery request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<UserDto?>();

        try
        {
            if (await _userRepository.GetAsync(cancellationToken, u => u.Email == request.Email) is { } user)
            {
                result.IsSuccess = true;
                result.StatusCode = 200;
                result.Message = "Data received successfully.";
                result.Data = user.Adapt<UserDto>();
            }
            else
            {
                result.IsSuccess = false;
                result.StatusCode = 404;
                result.Message = "Can't find your requested data.";
                result.AddError("1404", "Can't find your requested User Information.", "Email");
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