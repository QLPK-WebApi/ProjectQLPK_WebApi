using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces.Repositories;


public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByCodeAsync( string code, CancellationToken ct = default);
}