namespace Application.Common.Middelware
{
    using Domain.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class AutoLogoutMiddleware
    {
        private readonly RequestDelegate _next;

        public AutoLogoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<ApplicationUser> signInManager, IAuthService authService)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (!authService.GetCurrentUserAsync().Result.IsEnabled)
                {
                    await signInManager.SignOutAsync();
                    await context.Response.WriteAsync("<script>window.location.reload();</script>");
                    return;
                }
            }

            await _next(context);
        }
    }

}
