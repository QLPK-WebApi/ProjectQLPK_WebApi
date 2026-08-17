using App_QLPK.Application.DTO;

namespace App_QLPK.Application.Interfaces.Services;

public interface IDoctorService
{
    Task<DoctorDTO> CreateAsync(CreateDoctorDTO dto, CancellationToken ct = default);
}