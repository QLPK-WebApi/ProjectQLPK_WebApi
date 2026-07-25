
using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    /// <summary> Lấy danh sách có phân trang , kèm Patient/Doctor + User. Trả về (items, totalCount). </summary>
    Task<(IReadOnlyList<Appointment> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, string? keyword, CancellationToken ct = default);

    /// <summary>Check whether the doctor has any overlapping appointments (excluding the current appointment) (Kiểm tra bác sĩ đã có lịch trùng giờ chưa (loại trừ lịch hiện tại). 
    ///     ==> new CreateAppointment (tạo lịch hẹn mới)
    /// </summary>

    Task<bool> HasConflictAsync(int DoctorId, DateTime time, int? excludeId, CancellationToken ct = default);

    Task<bool> PatientExistsAsync(int patientId, CancellationToken ct = default);
    Task<bool> DoctorExistsAsync(int doctorId, CancellationToken ct = default);

    Task SaveChangesAsync(CancellationToken ct = default);
}