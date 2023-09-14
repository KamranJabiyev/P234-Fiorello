using Fiorello.Application.DTOs.AuthDtos;
using Fiorello.Domain.Entities;

namespace Fiorello.Application.Abstraction.Services;

public interface ITokenHandler
{
    Task<TokenResponseDto> GenerateTokenAsync(AppUser user);
}
