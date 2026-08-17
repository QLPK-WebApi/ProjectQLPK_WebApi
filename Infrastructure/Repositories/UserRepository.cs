

using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(QlpkDbContext context) : base(context)
    {
    }


    public Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        return _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username, ct);
    }
}





