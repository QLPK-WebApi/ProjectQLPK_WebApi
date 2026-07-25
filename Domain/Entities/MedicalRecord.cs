namespace App_QLPK.Domain.Entities;

public  class MedicalRecord : BaseEntity
{
    public int AppointmentId { get; set;}

    public string? Diagnosis { get; set;} // chuẩn đoán
    public string? Symptom { get; set;} // triệu chứng
    public string? Conclusion { get; set;} // kết luận
    public string? Notes { get; set;} // ghi chú


    public Appointment Appointment { get; set;} = null!;
    public ICollection<Prescription> Prescriptions { get; set;} = null!;
    public ICollection<ServiceOrder> ServiceOrders { get; set;} = new List<ServiceOrder>();
}