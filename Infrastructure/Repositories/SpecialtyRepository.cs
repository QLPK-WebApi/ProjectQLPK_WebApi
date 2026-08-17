using App_QLPK.Application.Interfaces;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Repositories;

public class SpecialtyRepository : RepositoryBase<Specialty>, ISpecialtyRepository
{
    public SpecialtyRepository(QlpkDbContext context) : base(context) { }

    public async Task<List<Specialty>> GetByIdAsync(List<int> id, CancellationToken ct = default)
    {
        if (id == null || id.Count == 0)
            return new List<Specialty>();
        
        return await _context.Specialties
            .Where(s => id.Contains(s.Id))
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct = default)
    {
        return await _context.Specialties
            .AnyAsync(s => s.Id == id, ct);
    }

    public Task<Specialty?> GetByNameAsync(string name, CancellationToken ct = default)
    {
        return _context.Specialties.FirstOrDefaultAsync(s => s.Name == name, ct);
    }

    
}