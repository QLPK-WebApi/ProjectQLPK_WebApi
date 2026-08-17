
using System.ComponentModel.DataAnnotations;

namespace App_QLPK.Application.DTO.Appointments;

public class UpdateAppointmentDTO
{
    [Required]
    public DateTime AppointmentTime { get; set; }

    [MaxLength(255)]
    public string? Reason { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}

public class UpdateAppointmentStatusDTO
{
    [Required]
    public string Status { get; set; } = null!;

    [MaxLength(255)]
    public string? CancelReason { get; set; }
}