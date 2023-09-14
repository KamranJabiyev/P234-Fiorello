using Fiorello.Application.Abstraction.Services;
using Fiorello.Application.DTOs.AuthDtos;
using Fiorello.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiorello.Infrastructure.Services;

public class TokenHandler : ITokenHandler
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;

    public TokenHandler(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<TokenResponseDto> GenerateTokenAsync(AppUser user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.NameIdentifier,user.Id)
        };
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(1);
        JwtSecurityToken jwt = new JwtSecurityToken(
            audience: _config["JWT:Audience"],
            issuer: _config["JWT:Issuer"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: credentials
        );
        string token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new TokenResponseDto() { Token = token, ExpireDate = expires };
    }
}
