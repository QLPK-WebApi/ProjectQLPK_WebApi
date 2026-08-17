using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces;

public interface ISpecialtyRepository : IRepository<Specialty>
{
    Task<Specialty?> GetByNameAsync(string name, CancellationToken ct = default);

    

    // Task<List<Specialty>> GetByIdAsync(List<int> id, CancellationToken ct = default);

    // Task<bool> ExistsAsync(int id, CancellationToken ct = default);
    
    // Task<List<Specialty>> GetAllAsync(CancellationToken ct = default);
}