namespace CleanArchitecture.AspNetCoreIdentity;
public interface ICurrentUserService
{
    string? UserId { get; }
}
