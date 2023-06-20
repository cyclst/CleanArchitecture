namespace TodoLists.Infrastructure.Identity;
public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }
}
