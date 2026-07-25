namespace App_QLPK.Domain.Entities;

public class Doctor : BaseEntity
{
    public int UserId { get; set; }
    public int SpecialtyId { get; set; } // khoa chính của bác sĩ

    public string? LicenseNumber { get; set; } // số giấy phép hành nghề
    public string? Biography { get; set; } // tiểu sử
    public bool IsActive { get; set; }
    
    

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public ICollection<DoctorSpecialty> DoctorSpecialties { get; set; } = new List<DoctorSpecialty>();

    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    public ICollection<ServiceResult> ServiceResults { get; set; } = new List<ServiceResult>();

    public User User { get; set; } = null!;
}