using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.DTO.Controller.AuthenticationController;

public class AuthenticationResultDto
{
    public string Token { get; set; } = default!;

    public string TokenType { get; set; } = default!;

    public DateTime? IssuedAt { get; set; }

    public DateTime? Expires { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RefreshToken { get; set; }
}