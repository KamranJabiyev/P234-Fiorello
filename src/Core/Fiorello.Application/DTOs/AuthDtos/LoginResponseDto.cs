namespace Fiorello.Application.DTOs.AuthDtos;

public class TokenResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
}
