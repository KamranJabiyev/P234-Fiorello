namespace Fiorello.Application.DTOs.AuthDtos;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
}
