﻿namespace Cyclst.CleanArchitecture.Application.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
