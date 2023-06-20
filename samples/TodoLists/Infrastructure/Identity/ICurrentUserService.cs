namespace TodoLists.Infrastructure.Identity;

public interface ICurrentUserService
{
    string? UserId { get; }
}
