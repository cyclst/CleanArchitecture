using System.Reflection;
using CleanArchitecture.Application.Persistence;
using CleanArchitecture.EntityFramework;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoLists.Domain.Entities;
using TodoLists.Infrastructure.Identity;
using TodoLists.Infrastructure.Persistence.Interceptors;
using TodoLists.Queries;

namespace TodoLists.Infrastructure.Persistence;

public class TodoListsDbContext : ApiAuthorizationDbContext<TodoListsUser>, ITodoListsDbContext, IDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public TodoListsDbContext(
        DbContextOptions<TodoListsDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options, operationalStoreOptions)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}
