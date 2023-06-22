namespace Cyclst.CleanArchitecture.Application.Logging;

public interface ILogDetailsService
{
    Task<string> GetDetails();
}
