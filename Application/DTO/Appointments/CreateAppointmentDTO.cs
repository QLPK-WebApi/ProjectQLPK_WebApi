

using System.ComponentModel.DataAnnotations;

namespace App_QLPK.Application.DTO.Appointments;

public class CreateAppointmentDTO
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentTime { get; set; }

    [MaxLength(255)]
    public string? Reason { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}