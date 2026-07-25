using App_QLPK.Domain.Enums;

namespace App_QLPK.Domain.Entities;

/// <summary>
/// Lịch hẹn khám
/// </summary>
public class Appointment : BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }

    public DateTime AppointmentTime { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }

    public  AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;  // tạo Enums để quản lý trạng thái của lịch hẹn(mặc định là đặt lịch)

    public DateTime CheckInTime { get; set; }
    public DateTime CompleteAt { get; set; }
    public string? CancelReason { get; set; }


    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public MedicalRecord? MedicalRecord { get; set; } // 1 lịch hẹn thì chưa có MedicaRecord
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}