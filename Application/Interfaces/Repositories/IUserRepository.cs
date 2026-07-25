
using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces.Repositories;



public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
}












