using Fiorello.Application.DTOs.AuthDtos;

namespace Fiorello.Application.Abstraction.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto newUser);
    Task<LoginResponseDto> Login(LoginDto user);
    Task Logout();
}
