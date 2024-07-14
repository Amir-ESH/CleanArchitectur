using Asp.Versioning;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Helper;
using CleanArchitecture.Application.DTO.Controller.AuthenticationController;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.DTO.User;
using Mapster;
using CleanArchitecture.Application.Features.User.Queries;

namespace CleanArchitecture.WebApi.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
[ApiController, Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IMediator mediator,
                                    IConfiguration configuration,
                                    ILogger<AuthenticationController> logger)
    {
        _mediator = mediator;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ResultDto<AuthenticationResultDto>>> Authenticate(
        AuthenticationRequestBodyDto request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<AuthenticationResultDto>();

        try
        {
            var userInfo = await ValidateUserCredentialAsync(request, cancellationToken);

            if (userInfo.IsSuccess)
            {
                if (userInfo.Data == null!)
                {
                    result.IsSuccess = false;
                    result.StatusCode = 401;
                    result.SetErrorDetails("1401", "Your login encountered an error.", "Password and Email");
                    result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
                }
                else
                {
                    if (!userInfo.Data!.IsActive)
                    {
                        result.IsSuccess = false;
                        result.StatusCode = 403;
                        result.SetErrorDetails("1403", "Your account is inactive.", "IsActive");
                        result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
                    }
                    else
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var privateKey = _configuration.GetSection("Authentication:SecretForKey").Value!
                                       + "Vd4bfjbRta7z4hGQfM4ACQpbq2FtEKUy3cIB2ecMcGHfBarbQwfdxjQjIbzhRGB";
                        var issuer = _configuration.GetSection("Authentication:Issuer").Value!;
                        var audience = _configuration.GetSection("Authentication:Audience").Value!;

                        var computerName = Dns.GetHostName();
                        var userAgent = Request.HttpContext.Request.Headers.UserAgent.ToString();

                        var claimForToken = new List<Claim>
                        {
                            new("Id", userInfo.Data!.Id.ToString()),
                            new("IpAddress", Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0"),
                            new("ComputerName", computerName),
                            new("UserAgent", userAgent),
                            new("IsActive", userInfo.Data!.IsActive.ToString()),
                        };

                        var now = DateTime.Now;

                        var expiration = request.RememberMe ? now.AddMinutes(30) : now.AddMinutes(43200);

                        var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(privateKey));

                        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claimForToken),
                            Expires = expiration,
                            SigningCredentials = signinCredentials,
                            NotBefore = now,
                            TokenType = "Bearer",
                            Issuer = issuer,
                            Audience = audience,
                            IssuedAt = now,
                            CompressionAlgorithm = "br"
                        };

                        result.StatusCode = 200;
                        result.IsSuccess = true;
                        result.Message = "Your login was successful.";
                        result.Data = new AuthenticationResultDto
                        {
                            Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                            TokenType = tokenDescriptor.TokenType,
                            IssuedAt = tokenDescriptor.IssuedAt,
                            Expires = tokenDescriptor.Expires
                        };
                    }
                }
            }
            else
            {
                result = userInfo.Adapt<ResultDto<AuthenticationResultDto>>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");

            result.IsSuccess = false;
            result.StatusCode = 401;
            result.ResponseType = "Exception error";
            result.SetErrorDetails("1401", "Your login encountered an error.", "Password and Email");
            result.SetTransactionDetails(Guid.NewGuid().ToString().Replace("-", ""), "Failed");
        }

        return StatusCode(result.StatusCode, result);
    }

    private async Task<ResultDto<UserDto?>> ValidateUserCredentialAsync(AuthenticationRequestBodyDto request,
                                                                        CancellationToken cancellationToken)
    {
        var result = new ResultDto<UserDto?>();

        try
        {
            result = await _mediator.Send(new GetUserByEmailAddressQuery(request.Email), cancellationToken);

            if (result.IsSuccess)
            {
                if (result.Data?.Password !=
                    PasswordHelper.HashPassword(request.Password,
                                                result.Data?.PublicKey,
                                                result.Data?.PrivateKey))
                {
                    result.IsSuccess = false;
                    result.StatusCode = 401;
                    result.SetErrorDetails("1401", "Your login encountered an error.", "Password and Email");
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
}
