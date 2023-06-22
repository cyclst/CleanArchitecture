namespace Cyclst.CleanArchitecture.Application.Logging;
public interface ILogDetailProvider
{
    Task<string> GetDetail();
}
