using Asp.Versioning;
using CleanArchitecture.Application.Common.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers.Common;

/// <summary>
/// Base CRUD Controller
/// </summary>
/// <typeparam name="TSoftDeleteRequest">Soft Delete Request Class</typeparam>
/// <typeparam name="TSoftDeleteDto">Soft Deleted DTO Result Class</typeparam>
/// <typeparam name="TDeleteRequest">Delete Request Class</typeparam>
/// <typeparam name="TDeleteDto">Deleted DTO Result Class</typeparam>
/// <typeparam name="TUpdateRequest">Update Request Class</typeparam>
/// <typeparam name="TUpdateDto">Update DTO Result Class</typeparam>
/// <typeparam name="TAddRequest">Add Request Class</typeparam>
/// <typeparam name="TAddDto">Add DTO Result Class</typeparam>
/// <typeparam name="TGetAllRequest">Get ALL Request Class</typeparam>
/// <typeparam name="TGetAllDto">Get ALL Result Class</typeparam>
/// <typeparam name="TGetByIdRequest">GetById Request Class</typeparam>
/// <typeparam name="TGetByIdDto">GetById DTO Result Class</typeparam>
//[ApiVersion("1.0", Deprecated = false)]
[ApiController/*, Route("api/v{version:apiVersion}/[controller]/[action]")*/]
public class GenericController
<TSoftDeleteRequest, TSoftDeleteDto,
 TDeleteRequest, TDeleteDto,
 TUpdateRequest, TUpdateDto,
 TAddRequest, TAddDto,
 TGetAllRequest, TGetAllDto,
 TGetByIdRequest, TGetByIdDto>
    : ControllerBase where TGetAllRequest : new()
{
    private readonly IMediator _mediator;

    public GenericController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<ActionResult<ResultDto<TGetByIdDto>>> GetByIdAsync(TGetByIdRequest query,
                                                                            CancellationToken cancellationToken)
    {
        var result = (ResultDto<TGetByIdDto>)(await _mediator.Send(query!, cancellationToken))!;

        return StatusCode(result.StatusCode, result);
    }

    protected async Task<ActionResult<TGetAllDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = (ResultDto<TGetAllDto>)(await _mediator.Send(new TGetAllRequest(), cancellationToken))!;

        return StatusCode(result.StatusCode, result);
    }

    protected async Task<ActionResult<ResultDto<TAddDto>>> AddAsync(TAddRequest command,
                                                                  CancellationToken cancellationToken)
    {
        var result = (ResultDto<TAddDto>)(await _mediator.Send(command!, cancellationToken))!;

        return StatusCode(result.StatusCode, result);
    }

    protected async Task<ActionResult<ResultDto<TUpdateDto>>> UpdateAsync(TUpdateRequest command,
                                                                          CancellationToken cancellationToken)
    {
        var result = (ResultDto<TUpdateDto>)(await _mediator.Send(command!, cancellationToken))!;

        return StatusCode(result.StatusCode, result);
    }

    protected async Task<ActionResult<ResultDto<TSoftDeleteDto>>> SoftDeleteAsync(TSoftDeleteRequest command,
        CancellationToken cancellationToken)
    {
        var result = (ResultDto<TSoftDeleteDto>)(await _mediator.Send(command!, cancellationToken))!;

        return StatusCode(result.StatusCode, result);
    }

    protected async Task<ActionResult<ResultDto<TDeleteDto>>> DeleteAsync(TDeleteRequest command,
        CancellationToken cancellationToken)
    {
        var result = (ResultDto<TDeleteDto>)(await _mediator.Send(command!, cancellationToken))!;

        return StatusCode(result.StatusCode, result);
    }
}
