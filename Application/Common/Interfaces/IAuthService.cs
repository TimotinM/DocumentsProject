using Domain.Entities;

public interface IAuthService
{
    Task<ApplicationUser> GetCurrentUserAsync();
}
