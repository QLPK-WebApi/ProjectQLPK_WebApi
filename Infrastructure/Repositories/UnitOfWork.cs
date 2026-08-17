

using App_QLPK.Application.Interfaces;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace App_QLPK.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly QlpkDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(QlpkDbContext context)
        => _context = context;

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);

        if (_transaction != null)
            await _transaction.CommitAsync(ct);
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        if (_transaction != null)
            await _transaction.RollbackAsync(ct);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }
}