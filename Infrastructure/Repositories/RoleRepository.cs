using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Repositories;


public class RoleRepository : RepositoryBase<Role>,IRoleRepository
{
    public RoleRepository(QlpkDbContext context) : base(context) { }

    public async Task<Role?> GetByCodeAsync(string code, CancellationToken ct)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Code == code, ct);
    }
}