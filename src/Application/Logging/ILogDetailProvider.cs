namespace CleanArchitecture.Application.Logging;
public interface ILogDetailProvider
{
    Task<string> GetDetail();
}
