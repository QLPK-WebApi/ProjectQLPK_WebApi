using App_QLPK.Domain.Enums;

namespace App_QLPK.Domain.Entities;


public class Invoice : BaseEntity
{
    public int PatientId { get; set;} 
    public int  AppointmentId { get; set;} // lịch khám khi nào, lịch tái khám

    public string InvoiceNumber { get; set;} = null!; // dùng để lưu số hóa đơn trên hệ thống   
    public InvoiceStatus Status { get; set;} = InvoiceStatus.Unpaid; // Mặc định chưa thanh toán

    public decimal TotalPrice { get; set;}
    public Patient Patient { get; set;} = null!;
    public Appointment Appointment { get; set;} = null!;
    public ICollection<Payment> Payments { get; set; } =  new List<Payment>();
    public ICollection<InvoiceDetail> InvoiceDetails { get; set;} = new List<InvoiceDetail>();
}