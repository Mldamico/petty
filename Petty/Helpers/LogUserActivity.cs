using Microsoft.AspNetCore.Mvc.Filters;
using Petty.Extensions;
using Petty.Interfaces;

namespace Petty.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.GetUserId();

        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var user = await repo.GetUserByIdAsync(int.Parse(userId));
        user.LastActive = DateTime.UtcNow;
        await repo.SaveAllAsync();
    }
}