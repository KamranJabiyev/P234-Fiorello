using Fiorello.Application.DTOs.AuthDtos;

namespace Fiorello.Application.Abstraction.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto newUser);
    Task<TokenResponseDto> LoginAsync(LoginDto user);
    Task Logout();
}
