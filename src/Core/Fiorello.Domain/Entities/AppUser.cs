using Microsoft.AspNetCore.Identity;

namespace Fiorello.Domain.Entities;

public class AppUser:IdentityUser
{
    public string? Fullname { get; set; }
    public bool IsActive { get; set; }
}
