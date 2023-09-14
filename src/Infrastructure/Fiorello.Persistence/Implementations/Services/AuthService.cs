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
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager,
                       ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
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

        return await _tokenHandler.GenerateTokenAsync(userDb);
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
