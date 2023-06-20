using CleanArchitecture.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.EntityFramework;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly IDbContext _dbContext;
    private readonly IMediator _mediator;

    public EFUnitOfWork(IDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(_dbContext);

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
