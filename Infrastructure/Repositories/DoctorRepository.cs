
using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Repositories;

public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
{
    public DoctorRepository(QlpkDbContext context) : base(context) { }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Username == username, ct);
    }
}
    


//     private IQueryable<Doctor> WithRelations()
//     {
//         return _context.Doctors
//             .Include(u => u.User)
//             .Include(d => d.DoctorSpecialties).ThenInclude(ds => ds.Specialty);
//     }


//     public async Task<(IReadOnlyList<Doctor> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, string? keyword, CancellationToken ct = default)
//     {
//         var query = WithRelations().AsNoTracking();

//         if (!string.IsNullOrEmpty(keyword))
//         {
//             var k = keyword.Trim();
//             query = query.Where(d => d.User != null && d.User.FullName.Contains(k));
//         }

//         var total = await query.CountAsync(ct);
//         var items = await query
//             .OrderBy(d => d.Id)
//             .Skip((pageIndex - 1) * pageSize)
//             .Take(pageSize)
//             .ToListAsync(ct);

//         return (items, total);
//     }

//     public async Task<Doctor?> GetByUserIdAsync(int id, CancellationToken ct = default)
//     {
//         return await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == id, ct);
//     }
// }