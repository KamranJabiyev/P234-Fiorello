namespace Fiorello.Application.DTOs.AuthDtos;

public class RegisterDto
{
    public string? Fullname { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set;}=null!;
    public string Email { get; set; }=null!;
}
