

using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App_QLPK.Infrastructure.Repositories;

public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(QlpkDbContext context) : base(context)
    {
    }
    
            // IQueryable<> -> the query is not excuted
    private IQueryable<Appointment> WithRelations() //Avoid multiple queries to load related data (tránh load từng cái riêng lẻ)
    {
        return _context.Appointments 
            .Include(a => a.Patient).ThenInclude(p => p.User)   // Appointment -> Patient -> User
            .Include(a => a.Doctor).ThenInclude(d => d.User);   // Appointment -> Doctor -> User
    }

    public async Task<(IReadOnlyList<Appointment> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, string? keyword, CancellationToken ct = default)
    {
        var query = WithRelations().AsNoTracking(); // off tracking ( tắt theo dõi của EF)

        if (!string.IsNullOrEmpty(keyword))
        {
            var k = keyword.Trim();
            query = query.Where(a => a.Patient.User.FullName.Contains(k) || a.Doctor.User.FullName.Contains(k) || 
            (a.Reason ?? "" ).Contains(k));
        }

        var total = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(a => a.AppointmentTime) // latest time 
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }




    public Task<bool> HasConflictAsync(int doctorId, DateTime time, int? excludeId, CancellationToken ct = default) 
            =>_context.Appointments.AnyAsync(a =>
        a.DoctorId == doctorId && a.AppointmentTime == time && a.Status != Domain.Enums.AppointmentStatus.Cancelled && (excludeId == null || a.Id != excludeId), ct);
    

    public Task<bool> PatientExistsAsync(int patientId, CancellationToken ct = default)
        => _context.Patients.AnyAsync(p => p.Id == patientId, ct);

    public Task<bool> DoctorExistsAsync(int doctorId, CancellationToken ct = default)
        => _context.Doctors.AnyAsync(d => d.Id == doctorId, ct);


    public Task SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);

}