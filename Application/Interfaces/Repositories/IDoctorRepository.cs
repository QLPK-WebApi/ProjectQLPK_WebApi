using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
}