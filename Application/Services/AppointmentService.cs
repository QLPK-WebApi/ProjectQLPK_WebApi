
using App_QLPK.Application.Common;
using App_QLPK.Application.DTO.Appointments;
using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Application.Interfaces.Services;
using App_QLPK.Application.Mapping;
using App_QLPK.Domain.Entities;
using App_QLPK.Domain.Enums;

namespace App_QLPK.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repo;

    public AppointmentService(IAppointmentRepository repo)
            => _repo = repo;


    public async Task<PagedResult<AppointmentDTO>> GetPagedAsync(int pageIndex, int pageSize, string? keyword, CancellationToken ct = default)
    {
        if (pageIndex < 1) pageIndex = 1; // nếu pageIndex < 1 thì gán = 1
        if (pageSize is < 1 or > 100) pageSize = 10; // gán = 10 nếu với đk đó

        var (items, total) = await _repo.GetPagedAsync(pageIndex, pageSize, keyword, ct);
        return new PagedResult<AppointmentDTO>
        {
            Items = items.Select(a => a.ToDto()).ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalCount = total,
        };
    }

    public async Task<AppointmentDTO?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var result = await _repo.GetByIdAsync(id, ct);
        return result?.ToDto();
    }

    public async Task<AppointmentDTO> CreateAsync(CreateAppointmentDTO dto, CancellationToken ct = default)
    {
        if (dto.AppointmentTime < DateTime.Now.AddMinutes(-1))
            throw new AppException("Thời gian hẹn không được ở quá khứ");

        if (!await _repo.PatientExistsAsync(dto.PatientId, ct))
            throw new AppException("Bệnh nhân không tồn tại.");

        if (!await _repo.DoctorExistsAsync(dto.DoctorId, ct))
            throw new AppException("Bác sĩ không tồn tại.");

        if (await _repo.HasConflictAsync(dto.DoctorId, dto.AppointmentTime, null, ct))
            throw new AppException("Bác sĩ đã có lịch hẹn khác vào thời điểm này.");

        var entity = new Appointment
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            AppointmentTime = dto.AppointmentTime,
            Reason = dto.Reason,
            Notes = dto.Notes,
            Status = Domain.Enums.AppointmentStatus.Scheduled,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync(ct);

        // reload attach Navigation --> join to table Patient / Doctor (tải lại kèm Navigation --> join đến bảng liên quan -> lấy ra name)
        var created = await _repo.GetByIdAsync(entity.Id, ct);
        return created!.ToDto();
    }

    public async Task<AppointmentDTO> UpdateAsync(int id, UpdateAppointmentDTO dto, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new AppException("Lịch không tồn tại.");

        if (entity.Status is Domain.Enums.AppointmentStatus.Completed or Domain.Enums.AppointmentStatus.Cancelled)
            throw new AppException("Không thể sửa lịch hẹn đã hoàn tất hoặc đã hủy.");

        if (await _repo.HasConflictAsync(entity.DoctorId, dto.AppointmentTime, id, ct))
            throw new AppException("Bác sĩ đã có lịch hẹn khác vào thời điểm này.");


        entity.AppointmentTime = dto.AppointmentTime;
        entity.Reason = dto.Reason;
        entity.Notes = dto.Notes;
        entity.UpdatedAt = DateTime.UtcNow;

        await _repo.SaveChangesAsync(ct);
        return entity.ToDto();
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new AppException("Lịch hẹn không tồn tại.");

        entity.Status = Domain.Enums.AppointmentStatus.Cancelled;
        entity.CancelReason ??= "Đã hủy bởi quản trị viên.";
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.SaveChangesAsync(ct);
    }

    public async Task<AppointmentDTO> ChangeStatusAsync(int id, UpdateAppointmentStatusDTO dto, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new AppException("Lịch hẹn không tồn tại.");

        if (!Enum.TryParse<AppointmentStatus>(dto.Status, ignoreCase: true, out var newStatus))
            throw new AppException($"Trạng thái '{dto.Status}' không hợp lệ");

        switch (newStatus)
        {
            case AppointmentStatus.CheckedIn:
                entity.CheckInTime = DateTime.UtcNow;
                break;

            case AppointmentStatus.Completed:
                entity.CompleteAt = DateTime.UtcNow;
                break;

            case AppointmentStatus.Cancelled:
                if (string.IsNullOrWhiteSpace(dto.CancelReason))
                    throw new AppException("Cần nhập lý đo khi hủy lịch hẹn.");
                entity.CancelReason = dto.CancelReason;
                break;
        }

        entity.Status = newStatus;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.SaveChangesAsync(ct);
        return entity.ToDto();
    }

    
}