using Fiorello.Application.Abstraction.Services;
using Fiorello.Application.DTOs.AuthDtos;
using Fiorello.Domain.Entities;
using Fiorello.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Fiorello.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;

    public AuthService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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
    public Task<LoginResponseDto> Login(LoginDto user)
    {
        throw new NotImplementedException();
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
