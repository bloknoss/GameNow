using GameNow.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
{
    private readonly UserManager<User> _userManager;

    public EmailVerifiedHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
    {
        var user = await _userManager.GetUserAsync(context.User);
        if (user != null && user.EmailConfirmed)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
