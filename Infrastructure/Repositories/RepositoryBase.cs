using App_QLPK.Application.Interfaces;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
{
    protected readonly QlpkDbContext _context;
    protected readonly DbSet<T> _dbSet;


    public RepositoryBase(QlpkDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity);
        
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Remove(entity);
    }

    public virtual Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(ct);
    }
}





// public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
// {
//     protected readonly QlpkDbContext _context;
//     protected readonly DbSet<T> _dbSet;

//     public RepositoryBase(QlpkDbContext context)
//     {
//         _context = context;
//         _dbSet = context.Set<T>();
//     }


//     public virtual Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
//     {
//         return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
//     }

//     public async Task AddAsync(T entity, CancellationToken ct = default)
//     {
//         await _dbSet.AddAsync(entity);
//         await _context.SaveChangesAsync(ct);
//     }

//     public async Task UpdateAsync(T entity, CancellationToken ct = default)
//     {
//         _dbSet.Update(entity);
//         await _context.SaveChangesAsync(ct);
//     }

//     public async Task DeleteAsync(T entity, CancellationToken ct = default)
//     {
//         _dbSet.Remove(entity);
//     }
// }