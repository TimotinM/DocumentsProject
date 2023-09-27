using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Domain.Entities;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        ClaimsPrincipal userClaimsPrincipal = _httpContextAccessor.HttpContext.User;

        if (userClaimsPrincipal.Identity.IsAuthenticated)
        {
            ApplicationUser user = await _userManager.GetUserAsync(userClaimsPrincipal);
            return user;
        }

        return null;
    }
}
