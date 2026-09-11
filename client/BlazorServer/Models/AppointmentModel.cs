using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models;
public class AppointmentModel
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = "";
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = "";
    public DateTime AppointmentTime { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = "";
    public DateTime? CheckInTime { get; set; }
    public DateTime? CompleteAt { get; set; }
    public string? CancelReason { get; set; }
    public DateTime CreatedAt { get; set; }
}


public class CreateAppointmentModel
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "PatientId phải > 0")]
    public int PatientId { get; set; }


    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "DoctorId phải > 0")]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentTime { get; set; } = DateTime.Now.AddHours(1);

    [MaxLength(255)]
    public string? Reason { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}