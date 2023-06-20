using System.Text;

namespace CleanArchitecture.Application.Logging;

public class LogDetailsService : ILogDetailsService
{
    private readonly IEnumerable<ILogDetailProvider> _detailsProvider;

    public LogDetailsService(IEnumerable<ILogDetailProvider> detailsProvider)
    {
        _detailsProvider = detailsProvider;
    }

    public async Task<string> GetDetails()
    {
        var sb = new StringBuilder();
        
        foreach (var item in _detailsProvider) { 
            sb.AppendFormat(" {0}", await item.GetDetail());
        }

        return sb.ToString();
    }
}
