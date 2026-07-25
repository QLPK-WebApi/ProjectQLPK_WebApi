namespace App_QLPK.Domain.Entities;

public class Patient : BaseEntity
{
    public int UserId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? BloodType { get; set; }  // nhóm máu
    public string? EmergencyName { get; set; } // tên người thân
    public string? EmergencyPhone { get; set; } // số điện thoại khẩn cho người thân
    public string? InsuranceNumber { get; set; }  // số BHYT


    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public User User { get; set; } = null!;
}