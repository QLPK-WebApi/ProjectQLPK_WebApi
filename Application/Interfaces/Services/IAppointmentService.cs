

using App_QLPK.Application.Common;
using App_QLPK.Application.DTO.Appointments;
using App_QLPK.Application.Mapping;

namespace App_QLPK.Application.Interfaces.Services;

public interface IAppointmentService
{
    Task<PagedResult<AppointmentDTO>> GetPagedAsync(int pageIndex, int pageSize, string? keyword, CancellationToken ct = default);
    Task<AppointmentDTO?> GetByIdAsync(int id, CancellationToken ct = default);


    Task<AppointmentDTO> CreateAsync(CreateAppointmentDTO dto, CancellationToken ct = default);
    Task<AppointmentDTO> UpdateAsync(int id,UpdateAppointmentDTO dto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}