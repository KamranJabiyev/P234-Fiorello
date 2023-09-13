using Fiorello.Application.Abstraction.Services;
using Fiorello.Application.DTOs.AuthDtos;
using Fiorello.Domain.Entities;
using Fiorello.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiorello.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager,
                       IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    public async Task RegisterAsync(RegisterDto newUser)
    {
        AppUser appUser = mapUser(newUser);
        var result= await _userManager.CreateAsync(appUser,newUser.Password);
        if (!result.Succeeded)
        {
            StringBuilder errorMessages = new();
            foreach (var error in result.Errors)
            {
                errorMessages.Append(error.Description);
            }
            throw new SignUpException(errorMessages.ToString());
        }
    }
    public async Task<TokenResponseDto> LoginAsync(LoginDto user)
    {
        AppUser userDb = await _userManager.FindByNameAsync(user.Username);
        if (userDb is null)
        {
            throw new SigninException("Username or Password is wrong");
        }

        var signinResult = await _signInManager.PasswordSignInAsync(userDb, user.Password, user.RememberMe, true);
        
        if (!signinResult.Succeeded)
        {
            throw new SigninException("Username or Password is wrong");
        }

        if (signinResult.IsLockedOut)
        {
            throw new AccountLockedOutException("Account is locked out");
        }

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name,user.Username),
            new Claim(ClaimTypes.NameIdentifier,userDb.Id)
        };
        var roles=await _userManager.GetRolesAsync(userDb);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role.ToString()));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(120);
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

    public Task Logout()
    {
        throw new NotImplementedException();
    }

    private AppUser mapUser(RegisterDto newUser)
    {
        return new AppUser()
        {
            Fullname = newUser.Fullname,
            UserName = newUser.Username,
            Email = newUser.Email
        };
    }
}
