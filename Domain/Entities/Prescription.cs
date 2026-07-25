using App_QLPK.Domain.Enums;

namespace App_QLPK.Domain.Entities;

/// <summary>
/// Đơn thuốc
/// </summary> 
public class Prescription : BaseEntity
{
    public int MedicalRecordId { get; set; }
    public int DoctorId { get; set; }

    public string? Notes { get; set; }
    public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Draft; // mặc định là đang tạo

    public MedicalRecord MedicalRecord = null!;
    public ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
}