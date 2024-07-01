﻿using Asp.Versioning;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.User;
using CleanArchitecture.Application.Features.User.Commands;
using CleanArchitecture.Application.Features.User.Queries;
using CleanArchitecture.WebApi.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
[ApiController, Route("api/v{version:apiVersion}/[controller]/[action]")]
public class UserController : GenericController
<SoftDeleteUserByIdRequest, bool,
    DeleteUserByIdRequest, bool,
    UpdateUserRequest, UserDto,
    AddUserRequest, UserDto,
    GetAllUserRequest, List<UserDto>,
    GetUserByIdRequest, UserDto>
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:required}")]
    public async Task<ActionResult<ResultDto<UserDto>>> GetById(Guid id, CancellationToken cancellationToken)
        => await GetByIdAsync(new GetUserByIdRequest(id), cancellationToken);

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll(CancellationToken cancellationToken)
        => await GetAllAsync(cancellationToken);

    [HttpPost]
    public async Task<ActionResult<ResultDto<UserDto>>> Add(AddUserRequest command, CancellationToken cancellationToken)
        => await AddAsync(command, cancellationToken);

    [HttpPut]
    public async Task<ActionResult<ResultDto<UserDto>>> Update(UpdateUserRequest command,
                                                               CancellationToken cancellationToken)
        => await UpdateAsync(command, cancellationToken);

    [HttpDelete("{id:required}")]
    public async Task<ActionResult<ResultDto<bool>>> SoftDelete(Guid id, CancellationToken cancellationToken)
        => await SoftDeleteAsync(new SoftDeleteUserByIdRequest(id), cancellationToken);

    [HttpDelete("{id:required}")]
    public async Task<ActionResult<ResultDto<bool>>> Delete(Guid id, CancellationToken cancellationToken)
        => await DeleteAsync(new DeleteUserByIdRequest(id), cancellationToken);
}