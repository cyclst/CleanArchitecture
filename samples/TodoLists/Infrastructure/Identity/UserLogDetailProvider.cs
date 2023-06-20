using CleanArchitecture.Application.Logging;
using MediatR;

namespace TodoLists.Infrastructure.Identity;
public class IdentityLogDetailProvider : ILogDetailProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public IdentityLogDetailProvider(ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<string> GetDetail()
    {
        var userId = _currentUserService.UserId ?? string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        return string.Format("{@UserId} {@UserName}", userId, userName);
    }
}
