using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.User;

namespace CleanArchitecture.Application.Features.User.Queries;

public record GetAllUserRequest : IRequest<ResultDto<List<UserDto>>>;